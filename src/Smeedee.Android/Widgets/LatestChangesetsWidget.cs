using Android.Content;
using Android.Text;
using Android.Util;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;
using Object = Java.Lang.Object;

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
            //Do();
            //var list = BuildList();
            //AddView(BuildList());
        }

        private void Do()
        {
            var commitList = FindViewById<ListView>(Resource.Id.LatestChangesetsList);
            LayoutInflater inflater = LayoutInflater.From(Context);
            inflater.Inflate(Resource.Layout.LatestChangesetsWidget_ListItem, commitList);
            
        }

        private ListView BuildList()
        {
            var commitList = FindViewById<ListView>(Resource.Id.LatestChangesetsList);
            foreach (var changeset in changesetService.GetLatestChangesets())
            {
                var view = new LinearLayout(Context);
                var img = new ImageView(Context);
                img.SetImageResource(Resource.Drawable.DefaultPerson);
                img.SetMaxHeight(50);
                img.SetMaxWidth(50);
                view.AddView(img);
                commitList.AddView(view);
            }
            return commitList;
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