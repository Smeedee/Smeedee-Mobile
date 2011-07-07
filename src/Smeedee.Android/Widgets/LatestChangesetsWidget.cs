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
    [WidgetAttribute("Latest Changesets", Resource.Drawable.Icon, IsEnabled = true)]
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
                keyValueMap.Add("Date", PrettyPrint(changeset.Date));
                listItems.Add(keyValueMap);
            }
            return listItems;

        }

        private string PrettyPrint(DateTime date)
        {
            return (DateTime.Now - date).ToHumanReadableString();
        }
    }
    public class FakeChangesetService : IChangesetService
    {
        public IEnumerable<Changeset> GetLatestChangesets()
        {
            return new []
                {
                    new Changeset() { Date = new DateTime(2011, 7, 4, 2, 0, 0), 
                                      User = "larspars", 
                                      Message = "Refactored HerpFactory.Derp()", },
                    new Changeset() { Date = new DateTime(2011, 7, 3, 2, 0, 0), 
                                      User = "larspars", 
                                      Message = "Fixed a lot, so this is a really long commit message. In this commit message I have also included several newlines \n\n 1) How will that look? \r\n 2) How about that? ",}
                           
                };
        }
    }

    public static class TimeSpanHelpers
    {
        public static string ToHumanReadableString(
            this TimeSpan ts)
        {
            const int SECOND = 1;
            const int MINUTE = 60 * SECOND;
            const int HOUR = 60 * MINUTE;
            const int DAY = 24 * HOUR;
            const int MONTH = 30 * DAY;

            var delta = ts.TotalSeconds;

            if (delta < 0)
            {
                return "not yet";
            }
            if (delta < 1 * MINUTE)
            {
                return ts.Seconds == 1 ? "one second ago" : ts.Seconds + " seconds ago";
            }
            if (delta < 2 * MINUTE)
            {
                return "a minute ago";
            }
            if (delta < 45 * MINUTE)
            {
                return ts.Minutes + " minutes ago";
            }
            if (delta < 90 * MINUTE)
            {
                return "an hour ago";
            }
            if (delta < 24 * HOUR)
            {
                return ts.Hours + " hours ago";
            }
            if (delta < 48 * HOUR)
            {
                return "yesterday";
            }
            if (delta < 30 * DAY)
            {
                return ts.Days + " days ago";
            }
            if (delta < 12 * MONTH)
            {
                int months = Convert.ToInt32(Math.Floor((double)ts.Days / 30));
                return months <= 1 ? "one month ago" : months + " months ago";
            }
            else
            {
                int years = Convert.ToInt32(Math.Floor((double)ts.Days / 365));
                return years <= 1 ? "one year ago" : years + " years ago";
            }
        }
    }
}