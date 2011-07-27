using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Preferences;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Android.Widgets.Settings;
using Smeedee.Model;
using Ids = Smeedee.Android.Resource.Id;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute(Name, StaticDescription = "Displays latest commits", SettingsType = typeof(LatestCommitsSettings))]
    public class LatestCommitsWidget : RelativeLayout, IWidget
    {
        public const string Name = "Latest commits";
        internal const string NoMessageTag = "(no message)";

        private LatestCommits model;
        private bool scrollDown = false;
        private TextColoringAdapterWithLoadMoreButton listAdapter;

        public event EventHandler DescriptionChanged;

        public LatestCommitsWidget(Context context) :
            base(context)
        {
            Initialize();
        }

        public LatestCommitsWidget(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            model = new LatestCommits();
            InflateLayout();
            Refresh();
        }

        private void InflateLayout()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                inflater.Inflate(Resource.Layout.LatestCommitsWidget, this);
            }
            else
            {
                throw new Exception("Unable to inflate view on Latest Changeset widget");
            }
        }

        public void Refresh()
        {
            scrollDown = false;
            model.Load(() => 
                ((Activity)Context).RunOnUiThread(() =>
                    {
                        Redraw();
                        OnDescriptionChanged(new EventArgs());
                    }));
        }

        public string GetDynamicDescription()
        {
            return model.DynamicDescription;
        }

        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }

        public void Redraw()
        {
            var listView = FindViewById<ListView>(Resource.Id.LatestCommitsList);
            var lastItemBeforeExpansion = Math.Max(0, listView.Count - 1);

            Log.Debug("Smeedee", "LC Redraw()");
            if (ShouldRecreateListAdapter())
            {
                listAdapter = CreateListAdapter();
            }

            Log.Debug("Smeedee", "model.HasMore: " + model.HasMore);
            listAdapter.ButtonEnabled = model.HasMore;
            listView.Adapter = listAdapter;

            if (scrollDown)
            {
                var xScroll = lastItemBeforeExpansion == 0
                                  ? 0
                                  : Height - 70;
                listView.SetSelectionFromTop(lastItemBeforeExpansion, xScroll);
            }
        }

        private TextColoringAdapterWithLoadMoreButton CreateListAdapter()
        {
            Log.Debug("Smeedee", "LC CreateListAdapter()");
            var from = new[] { "Image", "User", "Msg", "Date" };
            var to = new[] { Ids.LatestCommitsWidget_CommitterIcon, Ids.LatestCommitsWidget_ChangesetUser, Ids.LatestCommitsWidget_ChangesetText, Ids.LatestCommitsWidget_ChangesetDate };
            var listItems = CreateListItems();
            var layout = Resource.Layout.LatestCommitsWidget_ListItem;

            var adapter = new TextColoringAdapterWithLoadMoreButton(Context, listItems, layout, from, to, GetHighlightColor());
            adapter.LoadMoreClick += (o, e) =>
                                         {
                                             scrollDown = true;
                                             model.LoadMore(() => ((Activity) Context).RunOnUiThread(Redraw));
                                         };
            return adapter;
        }
        
        private Color GetHighlightColor()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Context);
            var highlightColor = prefs.GetString("lcs_HighlightColor", LatestCommitsSettings.DefaultRed);
            var highlightEnabled = prefs.GetBoolean("lcs_HighlightEnabled", true);
            return highlightEnabled ? ColorTools.GetColorFromHex(highlightColor) : Color.White;
        }

        private IList<IDictionary<string, object>> CreateListItems()
        {
            var data = new List<IDictionary<string, object>>();

            foreach (var changeSet in model.Commits)
            {
                var msg = (changeSet.Message == "") ? NoMessageTag : changeSet.Message;
                data.Add(new Dictionary<string, object>
                             {
                                 {"Msg", msg},
                                 {"Image", Resource.Drawable.DefaultPerson}, 
                                 {"User", changeSet.User}, 
                                 {"Date", (DateTime.Now - changeSet.Date).PrettyPrint()}
                             });
            }
            InsertDummyEntryForButton(data);
            return data;
        }

        private static void InsertDummyEntryForButton(List<IDictionary<string, object>> list)
        {
            list.Add(new Dictionary<string, object>  {
                                 {"Msg", ""},
                                 {"Image", Resource.Drawable.DefaultPerson}, 
                                 {"User", ""}, 
                                 {"Date", ""}
                             });
        }

        private int commitCountBeforeLoad = -1;
        private bool ShouldRecreateListAdapter()
        {
            if (listAdapter == null) return true;
            var modelWasChanged = commitCountBeforeLoad != model.Commits.Count();
            commitCountBeforeLoad = model.Commits.Count();
            return modelWasChanged;
        }
    }

    internal class TextColoringAdapterWithLoadMoreButton : SimpleAdapter 
    {
        private readonly Color highlightColor;
        private readonly Context context;
        public TextColoringAdapterWithLoadMoreButton(Context context, IList<IDictionary<string, object>> items, int resource, string[] from, int[] to, Color highlightColor) :
                                  base(context, items, resource, from, to)
        {
            this.context = context;
            this.highlightColor = highlightColor;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            //Force convertView to be null when it's an instance of RelativeLayout. 
            //This is our loadMoreLayout, and the base isn't able to recycle it. 
            convertView = convertView as LinearLayout; 
            var view = base.GetView(position, convertView, parent);

            if (position == Count - 1) return GetLoadMoreButton(); 

            return FindTextViewAndSetColor(view);
        }

        private View FindTextViewAndSetColor(View view)
        {
            if (!(view is LinearLayout)) return view;

            var linearLayout = (view as LinearLayout).GetChildAt(1);
            if (!(linearLayout is LinearLayout)) return view;

            var text = (linearLayout as LinearLayout).GetChildAt(1);
            if (!(text is TextView)) return view;

            var textView = text as TextView;
            var color = textView.Text == LatestCommitsWidget.NoMessageTag
                            ? highlightColor
                            : Color.White;
            textView.SetTextColor(color);

            return view;
        }

        private RelativeLayout loadMoreLayout;
        private Button loadMoreButton;
        private View GetLoadMoreButton()
        {
            if (loadMoreLayout == null)
            {
                loadMoreLayout = new RelativeLayout(context);
                var inflater = context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
                inflater.Inflate(Resource.Layout.LatestCommitsWidget_LoadMore, loadMoreLayout);

                loadMoreButton = loadMoreLayout.GetChildAt(0) as Button;
                loadMoreButton.Click += LoadMoreClick;
                loadMoreButton.Enabled = buttonEnabled;
            }
            return loadMoreLayout;
        }

        public event EventHandler LoadMoreClick;

        public override bool IsEnabled(int position)
        {
            return (position == Count - 1);
        }

        private bool buttonEnabled = true;
        public bool ButtonEnabled
        {   //Map directly to loadMoreLayout.Enabled when its instantiated,
            //and map to a backing field while it's not.
            get
            {
                if (false) return true;
                return loadMoreButton == null ? buttonEnabled : loadMoreButton.Enabled;
            }
            set
            {
                if (loadMoreButton == null) buttonEnabled = value;
                else loadMoreButton.Enabled = value;
            }
        }
    }
}