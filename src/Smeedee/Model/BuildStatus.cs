using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Services;

namespace Smeedee.Model
{
	public class BuildStatus : IModel
	{
        private readonly IBuildStatusService buildService = SmeedeeApp.Instance.ServiceLocator.Get<IBuildStatusService>();
	    private readonly IPersistenceService persistenceService = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();

        public BuildStatus()
        {
            ShowTriggeredBy = true;
            Ordering = BuildOrder.BuildTime;
            BrokenBuildsAtTop = true;
            builds = new List<Build>();
        }

        public void Load(Action callback)
        {
            buildService.Load(args =>
            {
                Builds = args.Result.ToList();
                callback();
            });
        }

        public BuildOrder Ordering { 
            get
            {
                var orderEnum = persistenceService.Get("buildSortOrdering", "projectname");
                if (orderEnum == "buildname") return BuildOrder.BuildName;
                if (orderEnum == "buildtime") return BuildOrder.BuildTime;
                return BuildOrder.BuildTime;
            } 
            set
            {
                if (value == BuildOrder.BuildName) persistenceService.Save("buildSortOrdering", "buildname");
                else if (value == BuildOrder.BuildTime) persistenceService.Save("buildSortOrdering", "buildtime");
            } 
        }

        public bool BrokenBuildsAtTop { 
            get 
            { 
                var brokenAtTop = persistenceService.Get("brokenBuildsAtTop", true);
                return brokenAtTop;
            } set
            {
                persistenceService.Save("brokenBuildsAtTop", value);
            }
        }

        public bool ShowTriggeredBy {
            get
            {
                var showTriggeredBy = persistenceService.Get("showTriggeredBy", true);
                return showTriggeredBy;
            }
            set
            {
                persistenceService.Save("showTriggeredBy", value);
            }
        }

	    private List<Build> builds;
        public List<Build> Builds { 
            get
            {
                var comparer = (Ordering == BuildOrder.BuildName) ? (IComparer<Build>)new BuildNameComparer() : new BuildTimeComparer();
                return BrokenBuildsAtTop ? GetOrderedBuildsWithBrokenFirst(comparer) : GetOrderedBuilds(comparer);
            } 
            private set
            {
                builds = value;
            } 
        }

        public int GetNumberOfBuildsByState(BuildState successState)
	    {
            return builds.Where(build => build.BuildSuccessState == successState).Count();
	    }

        private List<Build> GetOrderedBuilds(IComparer<Build> comparer)
        {
            return builds.OrderBy(b => b, comparer).ToList();
        }

        private List<Build> GetOrderedBuildsWithBrokenFirst(IComparer<Build> comparer)
        {
            var brokenBuilds = builds.Where(b => b.BuildSuccessState == BuildState.Broken).OrderBy(b => b, comparer).ToList();
            var workingBuilds = builds.Where(b => b.BuildSuccessState == BuildState.Working).OrderBy(b => b, comparer).ToList();
            var unknownBuilds = builds.Where(b => b.BuildSuccessState == BuildState.Unknown).OrderBy(b => b, comparer).ToList();

            var orderedBuilds = brokenBuilds;
            orderedBuilds.AddRange(workingBuilds);
            orderedBuilds.AddRange(unknownBuilds);
            return orderedBuilds;
        }
	}
}

