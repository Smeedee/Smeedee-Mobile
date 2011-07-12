using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Utilities;
using Ids = Smeedee.Android.Resource.Id;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Latest Changesets", Resource.Drawable.icon_smeedee, DescriptionStatic = "Shows latest commits")]
    public class LatestChangesetsWidget : RelativeLayout, IWidget
    {
        internal const string NoMessageTag = "(no message)";
        private IBackgroundWorker backgroundWorker;
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
            backgroundWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
            InflateLayout();
            Refresh();
        }

        private void GetData()
        {
            var changesetService = SmeedeeApp.Instance.ServiceLocator.Get<IChangesetService>();
            changesets = changesetService.GetLatestChangesets();
        }

        private void UpdateUI()
        {
            CreateListAdapter(changesets);
        }

        private void GetDataAndUpdateUI()
        {
            var changesetService = SmeedeeApp.Instance.ServiceLocator.Get<IChangesetService>();
            changesets = changesetService.GetLatestChangesets();
            ((Activity)Context).RunOnUiThread(() => CreateListAdapter(changesets));
        } 

        private void CreateListAdapter(IEnumerable<Changeset> changesets)
        {
            var commitList = FindViewById<ListView>(Resource.Id.LatestChangesetsList);

            var from = new[] {"Image", "User", "Msg", "Date"};
            var to = new[] {Ids.CommitterIcon, Ids.ChangesetUser, Ids.ChangesetText, Ids.ChangesetDate};

            var listItems = CreateListItems(changesets);

            var adapter = new TextColoringAdapter(Context, listItems, Resource.Layout.LatestChangesetsWidget_ListItem, from, to);
            commitList.Adapter = adapter;
        }

        private void InflateLayout()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.LatestChangesetsWidget, this);
        }

        private IList<IDictionary<string, object>> CreateListItems(IEnumerable<Changeset> changesets)
        {
            IList<IDictionary<String, object>> listItems = new List<IDictionary<String, object>>();

            foreach (var changeset in changesets)
            {
                IDictionary<String, object> keyValueMap = new Dictionary<String, object>();

                var msg = (changeset.Message == "") ? NoMessageTag : changeset.Message;
                keyValueMap["Msg"] = msg;
                keyValueMap["Image"] = Resource.Drawable.DefaultPerson;
                keyValueMap["User"] = changeset.User;
                keyValueMap["Date"] =  (DateTime.Now - changeset.Date).PrettyPrint();
                
                listItems.Add(keyValueMap);
            }

            return listItems;
        }

        public void Refresh()
        {
            backgroundWorker.Invoke(GetDataAndUpdateUI);

            //ContextSwitcher.Using((Activity)Context).
            //new ContextSwitcher((Activity)Context).InBackground(GetData).InUI(UpdateUI).Do();

        }
    }

    internal class TextColoringAdapter : SimpleAdapter 
    {
        public TextColoringAdapter(Context context, IList<IDictionary<string, object>> items, int resource, string[] from, int[] to) :
                                  base(context, items, resource, from, to)
        {
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
            var color = textView.Text == LatestChangesetsWidget.NoMessageTag ? Color.Red : Color.White;
            textView.SetTextColor(color);

            return view;
        }
}

    public class FakeChangesetService : IChangesetService
    {
        public IEnumerable<Changeset> GetLatestChangesets()
        {
            return new []
                {
                    new Changeset("Refactored HerpFactory.Derp()", new DateTime(2011, 7, 7, 12, 0, 0), "larmel"),
                    new Changeset("Fixed a lot, so this is a really long commit message. In this commit message I have also included several newlines \n\n 1) How will that look? \r\n 2) Should we shorten it? ", new DateTime(2011, 7, 7, 1, 10, 0), "larmel"),
                    new Changeset("", new DateTime(2011, 7, 6, 2, 0, 0), "larspars"),
                    new Changeset("Coded new codes.", new DateTime(2011, 7, 6, 1, 0, 0), "dagolap"),
                    new Changeset("Programmed them programs.", new DateTime(2011, 7, 5, 1, 0, 0), "rodsjo"),
                    new Changeset("Blabla", new DateTime(2011, 7, 7, 2, 0, 0), "larspars")
                };
        }
    }
}