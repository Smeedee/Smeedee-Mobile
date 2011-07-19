using System;
using System.Collections.Generic;
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
    [WidgetAttribute("Latest Changesets", StaticDescription = "Displays latest commits")]
    public class LatestChangesetsWidget : RelativeLayout, IWidget
    {
        internal const string NoMessageTag = "(no message)";
        private string _dynamicDescription;

        private LatestChangesets latestCommits;
        private ISharedPreferences pref;

        public LatestChangesetsWidget(Context context) :
            base(context)
        {
            pref = PreferenceManager.GetDefaultSharedPreferences(context);
            Initialize();
        }

        public LatestChangesetsWidget(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            latestCommits = new LatestChangesets();
            InflateLayout();
            Refresh();
        }

        private void InflateLayout()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                inflater.Inflate(Resource.Layout.LatestChangesetsWidget, this);
            }
            else
            {
                throw new Exception("Unable to inflate view on Latest Changeset widget");
            }
        }

        public void Refresh()
        {
            //TODO or not TODO?:
            //var count = int.Parse(pref.GetString("NumberOfCommitsDisplayed", "10"));
            latestCommits.Load(Redraw);
            RefreshDynamicDescription();
        }

        public void Redraw()
        {
            CreateListAdapter();
        }

        private void CreateListAdapter()
        {
            var commitList = FindViewById<ListView>(Resource.Id.LatestChangesetsList);

            var from = new[] { "Image", "User", "Msg", "Date" };
            var to = new[] { Ids.LatestChangesetWidget_CommitterIcon, Ids.LatestChangesetWidget_ChangesetUser, Ids.LatestChangesetWidget_ChangesetText, Ids.LatestChangesetWidget_ChangesetDate };

            var listItems = CreateListItems();

            var adapter = new TextColoringAdapter(Context, listItems, Resource.Layout.LatestChangesetsWidget_ListItem, from, to, GetHighlightColor());
            commitList.Adapter = adapter;
        }

        private void RefreshDynamicDescription()
        {
            _dynamicDescription = "Displaying latest " + pref.GetString("NumberOfCommitsDisplayed", "10") + " commits";
        }

        private Color GetHighlightColor()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Context);
            var highlightColor = prefs.GetString("lcs_HighlightColor", LatestChangesetsSettings.DefaultRed);
            var highlightEnabled = prefs.GetBoolean("lcs_HighlightEnabled", true);
            return highlightEnabled ? ColorTools.GetColorFromHex(highlightColor) : Color.White;
        }

        private IList<IDictionary<string, object>> CreateListItems()
        {
            var data = new List<IDictionary<string, object>>();

            foreach (var changeSet in latestCommits.Changesets)
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
            return data;
        }

        public string GetDynamicDescription()
        {
            return _dynamicDescription;
        }
    }

    internal class TextColoringAdapter : SimpleAdapter 
    {
        private readonly Color highlightColor;

        public TextColoringAdapter(Context context, IList<IDictionary<string, object>> items, int resource, string[] from, int[] to, Color highlightColor) :
                                  base(context, items, resource, from, to)
        {
            this.highlightColor = highlightColor;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
	        var view = base.GetView(position, convertView, parent);
            if (!(view is LinearLayout)) return view;

            var linearLayout = (view as LinearLayout).GetChildAt(1);
            if (!(linearLayout is LinearLayout)) return view;
            
            var text = (linearLayout as LinearLayout).GetChildAt(1);
            if (!(text is TextView)) return view;
            
            var textView = text as TextView;
            var color = textView.Text == LatestChangesetsWidget.NoMessageTag
                            ? highlightColor
                            : Color.White;
            textView.SetTextColor(color);

            return view;
        }

        public override bool IsEnabled(int position)
        {
            return false;
        }
    }
}