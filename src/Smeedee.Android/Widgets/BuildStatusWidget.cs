using System;
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
        private readonly string[] listItemMappingFrom = new[] { "project_name", "username", "datetime" };
        private readonly int[] listItemMappingTo = new[] {  Resource.Id.projectname, Resource.Id.username, Resource.Id.datetime };
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
            FillBuildList();
        }
        
        private void CreateGui()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.BuildStatusWidget, this);
        }

        private void FillBuildList()
        {
            bgWorker.Invoke(() => {
                var buildList = FindViewById<ListView>(Resource.Id.build_list);

                var adapter = new SimpleAdapter(Context, GetData(), Resource.Layout.BuildStatusWidget_ListItem, listItemMappingFrom, listItemMappingTo);
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
                                                          {"datetime", build.BuildTime}
                                                      };
                fillMaps.Add(map);
            }
            return fillMaps;
        }
    }
}
