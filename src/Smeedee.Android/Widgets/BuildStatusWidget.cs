using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Smeedee.Android.Widgets.Settings;
using Smeedee.Lib;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Build Status", StaticDescription = "Shows build status for each project", SettingsType = typeof(BuildStatusSettings))]
    public class BuildStatusWidget : RelativeLayout, IWidget
    {
        private readonly string[] listItemMappingFrom = new[] { "project_name", "username", "datetime", "success_status" };
        private readonly int[] listItemMappingTo = new[] { Resource.Id.BuildStatuswidget_projectname, Resource.Id.BuildStatuswidget_username, Resource.Id.BuildStatuswidget_datetime, Resource.Id.BuildStatuswidget_buildstatus };

        private ListView buildList;
        private BuildStatus model;
        private DateTime _lastRefreshTime;
        private IPersistenceService persistence;

        public event EventHandler DescriptionChanged;

        public BuildStatusWidget(Context context) : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            model = new BuildStatus();
            CreateGui();
            buildList = FindViewById<ListView>(Resource.Id.BuildStatusWidget_build_list);
            persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
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

        private IList<IDictionary<string, object>> GetListAdapterFromBuildModels()
        {
            var showTriggeredBy = persistence.Get("BuildStatus.ShowTriggeredBy", true);

            return model.Builds.Select(build => new Dictionary<String, object>
                        {
                            {"project_name", build.ProjectName},
                            {"username", (showTriggeredBy) ? build.Username : ""},
                            {"datetime", (DateTime.Now - build.BuildTime).PrettyPrint() }, 
                            { "success_status", (int) build.BuildSuccessState}
                        }).Cast<IDictionary<string, object>>().ToList();
        }
        public void Refresh()
        {
            model.Load(() =>
                       ((Activity) Context).RunOnUiThread(() =>
                        {
                            buildList.Adapter = new BuildStatusAdapter(
                            Context,
                            GetListAdapterFromBuildModels(),
                            Resource.Layout.BuildStatusWidget_ListItem,
                            listItemMappingFrom,
                            listItemMappingTo);

                            OnDescriptionChanged(new EventArgs());
                        }));
            _lastRefreshTime = DateTime.Now;
        }

        public DateTime LastRefreshTime()
        {
            return _lastRefreshTime;
        }

        public string GetDynamicDescription()
        {
            return model.DynamicDescription;
        }

        private void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
    }

    internal class BuildStatusAdapter : SimpleAdapter
    {
        public BuildStatusAdapter(IntPtr doNotUse) 
            : base(doNotUse)
        {
        }

        public BuildStatusAdapter(Context context, IList<IDictionary<string, object>> data, int resource, string[] from, int[] to) 
            : base(context, data, resource, from, to)
        {
        }

        public override bool IsEnabled(int position) // To disable list item clicks
        { return false; }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = base.GetView(position, convertView, parent);
            var status = int.Parse(((TextView)view.FindViewById(Resource.Id.BuildStatuswidget_buildstatus)).Text);
            var buildStatusView = view.FindViewById(Resource.Id.BuildStatuswidget_buildstatusdisplay) as ImageView;
            
            if (buildStatusView != null)
            {
                if (status == (int)BuildState.Working)
                    buildStatusView.SetImageResource(Resource.Drawable.icon_buildsuccess);
                if (status == (int)BuildState.Broken)
                    buildStatusView.SetImageResource(Resource.Drawable.icon_buildfailure);
                if (status == (int)BuildState.Unknown)
                    buildStatusView.SetImageResource(Resource.Drawable.icon_buildunknown);
            } 
            return view;
        }
    }
}
