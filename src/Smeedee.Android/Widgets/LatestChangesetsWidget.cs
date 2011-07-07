using System;
using System.Collections.Generic;
using Android.Content;
using Android.Graphics;
using Android.Text;
using Android.Text.Style;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;
using Ids = Smeedee.Android.Resource.Id;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Latest Changesets", "@drawable/icon", IsEnabled = true)]
    public class LatestChangesetsWidget : RelativeLayout, IWidget
    {
        private IChangesetService changesetService;
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
            changesetService = new FakeChangesetService();
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.LatestChangesetsWidget, this);

            var commitList = FindViewById<ListView>(Resource.Id.LatestChangesetsList);

            var from = new[] { "Image", "User", "Text", "Date" };
            var to = new[] { Ids.CommitterIcon, Ids.ChangesetUser, Ids.ChangesetText, Ids.ChangesetDate };

            var listItems = BuildList();

            var adapter = new SimpleAdapter(Context, listItems, Resource.Layout.LatestChangesetsWidget_ListItem, from, to);
            commitList.Adapter = adapter;
        }

        private IList<IDictionary<string, object>> BuildList()
        {
            IList<IDictionary<String, object>> listItems = new List<IDictionary<String, object>>();

            foreach (var changeset in changesetService.GetLatestChangesets())
            {
                IDictionary<String, object> keyValueMap = new Dictionary<String, object>();
                keyValueMap.Add("Image", Resource.Drawable.DefaultPerson);
                keyValueMap.Add("User", changeset.User);
                keyValueMap.Add("Text", changeset.Message);
                keyValueMap.Add("Date", changeset.Date);
                listItems.Add(keyValueMap);
            }
            return listItems;

        }
    }
    public class FakeChangesetService : IChangesetService
    {
        public IEnumerable<Changeset> GetLatestChangesets()
        {
            return new []
                {
                    new Changeset() { Date = DateTime.Now, User = "larspars", 
                                      Message = "Refactored HerpFactory.Derp()", },
                    new Changeset() { Date = DateTime.Now, User = "larspars", 
                                      Message = "Fixed a lot, so this is a really long commit message. In this commit message I have also included several newlines \n\n 1) How will that look? \r\n 2) How about that? ",}
                           
                };
        }
    }
}