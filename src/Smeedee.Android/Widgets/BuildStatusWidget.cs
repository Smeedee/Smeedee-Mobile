using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Build Status", StaticDescription = "Shows build status for each project")]
    public class BuildStatusWidget : RelativeLayout, IWidget
    {
        private readonly string[] listItemMappingFrom = new[] { "project_name", "username", "datetime", "success_status" };
        private readonly int[] listItemMappingTo = new[] { Resource.Id.BuildStatuswidget_projectname, Resource.Id.BuildStatuswidget_username, Resource.Id.BuildStatuswidget_datetime, Resource.Id.BuildStatuswidget_buildstatus };
        private readonly IModelService<BuildStatus> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<BuildStatus>>();
        private readonly IBackgroundWorker bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        private ListView buildList;
        private BuildStatus model;
        private string _dynamicDescription = "";

        public BuildStatusWidget(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
            CreateGui();
            buildList = FindViewById<ListView>(Resource.Id.BuildStatusWidget_build_list);
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

        private void RefreshBuildsFromServer()
        {
            model = service.Get(CreateServiceArgsDictionary());
            RefreshUiBuildList();
        }

        private void RefreshDynamicDescription()
        {
            var numberOfWorkingBuilds = model.GetNumberOfBuildsThatHaveState(BuildState.Working);
            var numberOfBrokenBuilds = model.GetNumberOfBuildsThatHaveState(BuildState.Broken);
            var numberOfUnknownBuilds = model.GetNumberOfBuildsThatHaveState(BuildState.Unknown);
            var numberOfBuilds = model.Builds.Count();
            
            if (numberOfBuilds == 0)
                _dynamicDescription = "No builds fetched from the Smeedee Server";
            else
            {
                if (numberOfWorkingBuilds == 0 && numberOfUnknownBuilds == 0)
                    _dynamicDescription = "OMG! All builds are broken!";
                else
                {
                    if (numberOfWorkingBuilds > 0)
                        _dynamicDescription = numberOfWorkingBuilds + " working";
                    if (numberOfBrokenBuilds > 0)
                        _dynamicDescription += ", " + numberOfBrokenBuilds + " broken";
                    if (numberOfUnknownBuilds > 0)
                    {
                        _dynamicDescription += ", " + numberOfUnknownBuilds + " unknown";
                    }

                    _dynamicDescription += " builds";
                }
            }
        }

        private void RefreshUiBuildList()
        {
            ((Activity)Context).RunOnUiThread(() =>
            {
                var adapter = new BuildStatusAdapter(Context, GetListAdapterFromBuildModels(), Resource.Layout.BuildStatusWidget_ListItem, listItemMappingFrom, listItemMappingTo);
                buildList.Adapter = adapter;
                RefreshDynamicDescription();
            });
        }

        private IList<IDictionary<string, object>> GetListAdapterFromBuildModels()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Context);
            var showTriggeredBy = prefs.GetBoolean("showTriggeredBy", true);
            var brokenFirst = prefs.GetBoolean("brokenBuildsAtTop", true);
            var orderBy = prefs.GetString("buildSortOrdering", "buildname");

            var comparer = (orderBy == "buildname") ? (IComparer<Build>)new BuildNameComparer() : new BuildTimeComparer();

            List<Build> orderedBuilds = brokenFirst ? GetOrderedBuildsWithBrokenFirst(comparer) : GetNormalOrderedBuilds(comparer);

            return orderedBuilds.Select(build => new Dictionary<String, object>
                        {
                            {"project_name", build.ProjectName},
                            {"username", (showTriggeredBy) ? build.Username : ""},
                            {"datetime", (DateTime.Now - build.BuildTime).PrettyPrint() }, 
                            { "success_status", (int) build.BuildSuccessState}
                        }).Cast<IDictionary<string, object>>().ToList();
        }

        private List<Build> GetNormalOrderedBuilds(IComparer<Build> comparer)
        {
            return model.Builds.OrderBy(b => b, comparer).ToList();
        }

        private List<Build> GetOrderedBuildsWithBrokenFirst(IComparer<Build> comparer)
        {
            var brokenBuilds = model.Builds.Where(b => b.BuildSuccessState == BuildState.Broken).OrderBy(b => b, comparer);
            var workingBuilds = model.Builds.Where(b => b.BuildSuccessState == BuildState.Working).OrderBy(b => b, comparer);
            var unknownBuilds = model.Builds.Where(b => b.BuildSuccessState == BuildState.Unknown).OrderBy(b => b, comparer);

            var orderedBuilds = brokenBuilds.ToList();
            orderedBuilds.AddRange(workingBuilds);
            orderedBuilds.AddRange(unknownBuilds);
            return orderedBuilds;
        }

        private Dictionary<string, string> CreateServiceArgsDictionary()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(Context);
            var args = new Dictionary<string, string>();

            args["ordering"] = prefs.GetString("buildSortOrdering", "buildtime");
            args["brokenBuildsAtTop"] = prefs.GetBoolean("brokenBuildsAtTop", true).ToString();

            return args;
        }

        public void Refresh()
        {
            bgWorker.Invoke(RefreshBuildsFromServer);
        }

        public string GetDynamicDescription()
        {
            return _dynamicDescription;
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

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var view = convertView ?? base.GetView(position, convertView, parent);
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

        public override bool IsEnabled(int position)
        {
            return false;
        }
    }


    public class BuildTimeComparer : IComparer<Build>
    {
        public int Compare(Build x, Build y)
        {
            return x.BuildTime > y.BuildTime ? 1 : -1;
        }
    }

    public class BuildNameComparer : IComparer<Build>
    {
        public int Compare(Build x, Build y)
        {
            return string.Compare(x.ProjectName, y.ProjectName);
        }
    }
}
