using System;
using System.Collections;
using System.Collections.Generic;
using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Utilities;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Build Status", Resource.Drawable.icon_projectstatus, IsEnabled = true)]
    public class BuildStatusWidget : RelativeLayout, IWidget
    {
        private readonly string[] listItemMappingFrom = new[] { "project_name", "username", "datetime", "success_status" };
        private readonly int[] listItemMappingTo = new[] {  Resource.Id.projectname, Resource.Id.username, Resource.Id.datetime, Resource.Id.buildstatus };
        private readonly IModelService<BuildStatus> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<BuildStatus>>();
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        public BuildStatusWidget(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateGui();
            bgWorker.Invoke(FillBuildList);
        }
        
        private void CreateGui()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.BuildStatusWidget, this);
        }

        private void FillBuildList()
        {
            var buildList = FindViewById<ListView>(Resource.Id.build_list);

            if (buildList == null) return;

            var adapter = new BuildStatusAdapter(Context, GetData(), Resource.Layout.BuildStatusWidget_ListItem, listItemMappingFrom, listItemMappingTo);
            Handler.Post(() =>
                             {
                                buildList.Adapter = adapter;
                             });
        }

        private IList<IDictionary<string, object>> GetData()
        {
            IList<IDictionary<String, object>> fillMaps = new List<IDictionary<String, object>>();
            foreach (var build in service.Get())
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
            var buildStatusView = view.FindViewById(Resource.Id.buildstatusdisplay) as TextView;
            
            if (buildStatusView != null)
            {
                if (status == (int)BuildSuccessState.Success)
                    buildStatusView.SetBackgroundResource(Resource.Color.build_green);
                if (status == (int)BuildSuccessState.Failure)
                    buildStatusView.SetBackgroundResource(Resource.Color.build_red);
                if (status == (int)BuildSuccessState.Unknown)
                    buildStatusView.SetBackgroundResource(Resource.Color.build_orange);
            } 
            return view;
        }
    }
}
