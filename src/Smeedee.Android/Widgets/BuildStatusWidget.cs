using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Utilities;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Build Status", Resource.Drawable.icon_projectstatus, DescriptionStatic = "Shows build status for projects", IsEnabled = true)]
    public class BuildStatusWidget : RelativeLayout, IWidget
    {
        private readonly string[] listItemMappingFrom = new[] { "project_name", "username", "datetime", "success_status" };
        private readonly int[] listItemMappingTo = new[] {  Resource.Id.projectname, Resource.Id.username, Resource.Id.datetime, Resource.Id.buildstatus };
        private readonly IModelService<BuildStatus> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<BuildStatus>>();
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private ListView buildList;

        public BuildStatusWidget(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateGui();
            buildList = FindViewById<ListView>(Resource.Id.build_list);

            bgWorker.Invoke(FillBuildList);
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

        private void FillBuildList()
        {
            var data = GetData();
            ((Activity)Context).RunOnUiThread(() =>
            {
                var adapter = new BuildStatusAdapter(Context, data, Resource.Layout.BuildStatusWidget_ListItem, listItemMappingFrom, listItemMappingTo);
                buildList.Adapter = adapter;
            });
        }

        private IList<IDictionary<string, object>> GetData()
        {
            IList<IDictionary<String, object>> fillMaps = new List<IDictionary<String, object>>();
            var args = new Dictionary<string, string>();
            foreach (var build in service.Get(args))
            {
                IDictionary<String, object> map = new Dictionary<String, object>
                                                      {
                                                          {"project_name", build.ProjectName},
                                                          {"username", build.Username},
                                                          {"datetime", build.BuildTime},
                                                          {"success_status", (int)build.BuildSuccessState}
                                                      };
                fillMaps.Add(map);
            }
            return fillMaps;
        }

        public void Refresh()
        {
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
            var status = int.Parse(((TextView) view.FindViewById(Resource.Id.buildstatus)).Text);
            var buildStatusView = view.FindViewById(Resource.Id.buildstatusdisplay) as ImageView;
            
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
