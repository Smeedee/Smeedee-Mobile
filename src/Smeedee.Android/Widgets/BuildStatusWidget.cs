using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Widget;

using Smeedee.Model;
using Smeedee;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Build Status", DescriptionStatic = "Shows build status for projects")]
    public class BuildStatusWidget : RelativeLayout, IWidget
    {
        private readonly string[] listItemMappingFrom = new[] { "project_name", "username", "datetime", "success_status" };
        private readonly int[] listItemMappingTo = new[] { Resource.Id.BuildStatuswidget_projectname, Resource.Id.BuildStatuswidget_username, Resource.Id.BuildStatuswidget_datetime, Resource.Id.BuildStatuswidget_buildstatus };
        private readonly IModelService<BuildStatus> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<BuildStatus>>();
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private ListView buildList;
        private IEnumerable<BuildStatus> builds;

        public BuildStatusWidget(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateGui();
            FindBuildListUi();

            Refresh();
        }

        private void CreateGui()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            if (inflater != null)
            {
                inflater.Inflate(Resource.Layout.BuildStatusWidget, this);
            } else
            {
                throw new NullReferenceException("Failed to get inflater in Build status widget");
            }
        }
        
        private void FindBuildListUi()
        {
            buildList = FindViewById<ListView>(Resource.Id.BuildStatusWidget_build_list);
        }

        private void RefreshBuildsFromServer()
        {
            builds = service.Get(CreateServiceArgsDictionary());
            RefreshUiBuildList();
        }

        private void RefreshUiBuildList()
        {
            ((Activity)Context).RunOnUiThread(() =>
            {
                var adapter = new BuildStatusAdapter(Context, GetListAdapterFromBuildModels(), Resource.Layout.BuildStatusWidget_ListItem, listItemMappingFrom, listItemMappingTo);
                buildList.Adapter = adapter;
            });
        }

        private IList<IDictionary<string, object>> GetListAdapterFromBuildModels()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Context);
            var showTriggeredBy = prefs.GetBoolean("showTriggeredBy", true);

            return builds.Select(build => new Dictionary<String, object>
                        {
                            {"project_name", build.ProjectName},
                            {"username", (showTriggeredBy) ? build.Username : ""},
                            {"datetime", (DateTime.Now - build.BuildTime).PrettyPrint() }, 
                            { "success_status", (int) build.BuildSuccessState}
                        }).Cast<IDictionary<string, object>>().ToList();
        }

        private Dictionary<string, string> CreateServiceArgsDictionary()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Context);
            var args = new Dictionary<string, string>();

            args["sorting"] = prefs.GetString("buildSortOrdering", "buildtime");
            args["brokenBuildsAtTop"] = prefs.GetBoolean("brokenBuildsAtTop", true).ToString();

            return args;
        }

        public void Refresh()
        {
            bgWorker.Invoke(RefreshBuildsFromServer);
        }
    }

    internal class BuildStatusAdapter : SimpleAdapter
    {
        public BuildStatusAdapter(IntPtr doNotUse) : base(doNotUse)
        {
        }

        public BuildStatusAdapter(Context context, IList<IDictionary<string, object>> data, int resource, string[] from, int[] to) : base(context, data, resource, from, to)
        {
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);
            var status = int.Parse(((TextView)view.FindViewById(Resource.Id.BuildStatuswidget_buildstatus)).Text);
            var buildStatusView = view.FindViewById(Resource.Id.BuildStatuswidget_buildstatusdisplay) as ImageView;
            
            if (buildStatusView != null)
            {
                if (status == (int)BuildSuccessState.Success)
                    buildStatusView.SetImageResource(Resource.Drawable.icon_buildsuccess);
                if (status == (int)BuildSuccessState.Failure)
                    buildStatusView.SetImageResource(Resource.Drawable.icon_buildfailure);
                if (status == (int)BuildSuccessState.Unknown)
                    buildStatusView.SetImageResource(Resource.Drawable.icon_buildunknown);
            } 
            return view;
        }
    }
}
