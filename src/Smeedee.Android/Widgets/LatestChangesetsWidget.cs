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
using Smeedee.Model;
using Smeedee;
using Ids = Smeedee.Android.Resource.Id;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Latest Changesets", DescriptionStatic = "Shows latest commits")]
    public class LatestChangesetsWidget : RelativeLayout, IWidget
    {
        internal const string NoMessageTag = "(no message)";
        private const string DefaultRed = "dc322f";
        private IEnumerable<Changeset> changesets;

        public LatestChangesetsWidget(Context context) :
            base(context)
        {
            Initialize();
        }

        public LatestChangesetsWidget(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            InflateLayout();
            Refresh();
        }
        
        private Color GetHighlightColor()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Context);
            var highlightColor = prefs.GetString("lcs_HighlightColor", DefaultRed);
            var highlightEnabled = prefs.GetBoolean("lcs_HighlightEnabled", true);
            return highlightEnabled ? ColorTools.FromHex(highlightColor) : Color.White;
        }

        private void GetData()
        {
            var changesetService = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<Changeset>>();
            changesets = changesetService.Get(null);
        }

        private void UpdateUI()
        {
            CreateListAdapter();
        }

        private void CreateListAdapter()
        {
            var commitList = FindViewById<ListView>(Resource.Id.LatestChangesetsList);

            var from = new[] {"Image", "User", "Msg", "Date"};
            var to = new[] { Ids.LatestChangesetWidget_CommitterIcon, Ids.LatestChangesetWidget_ChangesetUser, Ids.LatestChangesetWidget_ChangesetText, Ids.LatestChangesetWidget_ChangesetDate };

            var listItems = CreateListItems();

            var adapter = new TextColoringAdapter(Context, listItems, Resource.Layout.LatestChangesetsWidget_ListItem, from, to, GetHighlightColor());
            commitList.Adapter = adapter;
        }

        private void InflateLayout()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.LatestChangesetsWidget, this);
        }

        private IList<IDictionary<string, object>> CreateListItems()
        {
            return (from changeset in changesets
                    let msg = (changeset.Message == "") ? NoMessageTag : changeset.Message
                    select new Dictionary<string, object>()
                               {
                                   {"Msg", msg}, {"Image", Resource.Drawable.DefaultPerson}, {"User", changeset.User}, {"Date", (DateTime.Now - changeset.Date).PrettyPrint()}
                               }).Cast<IDictionary<string, object>>().ToList();
        }

        public void Refresh()
        {
            ContextSwitcher.Using((Activity) Context).InBackground(GetData).InUI(UpdateUI).Run();
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
    }
}