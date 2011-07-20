using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Services;

namespace Smeedee.Model
{
	public class BuildStatus
	{
        public const string SortingPropertyKey = "BuildStatus.Sorting";
        public const string BrokenFirstPropertyKey = "BuildStatus.BrokenFirst";
	    public const string ShowTriggeredByPropertyKey = "BuildStatus.ShowTriggeredBy";

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
                var orderEnum = persistenceService.Get(SortingPropertyKey, "projectname");
                if (orderEnum == "buildname") return BuildOrder.BuildName;
                if (orderEnum == "buildtime") return BuildOrder.BuildTime;
                return BuildOrder.BuildTime;
            } 
            set
            {
                if (value == BuildOrder.BuildName) persistenceService.Save(SortingPropertyKey, "buildname");
                else if (value == BuildOrder.BuildTime) persistenceService.Save(SortingPropertyKey, "buildtime");
            } 
        }

        public bool BrokenBuildsAtTop { 
            get 
            { 
                var brokenAtTop = persistenceService.Get(BrokenFirstPropertyKey, true);
                return brokenAtTop;
            } set
            {
                persistenceService.Save(BrokenFirstPropertyKey, value);
            }
        }

        public bool ShowTriggeredBy {
            get
            {
                var showTriggeredBy = persistenceService.Get(ShowTriggeredByPropertyKey, true);
                return showTriggeredBy;
            }
            set
            {
                persistenceService.Save(ShowTriggeredByPropertyKey, value);
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

	    public string DynamicDescription 
        { 
            get
	        {
                var numberOfWorkingBuilds = GetNumberOfBuildsByState(BuildState.Working);
                var numberOfBrokenBuilds = GetNumberOfBuildsByState(BuildState.Broken);
                var numberOfUnknownBuilds = GetNumberOfBuildsByState(BuildState.Unknown);
                var numberOfBuilds = builds.Count();

                if (numberOfBuilds == 0)
                    return "No builds fetched from the Smeedee Server";
	            if (numberOfWorkingBuilds == 0 && numberOfUnknownBuilds == 0)
	                return "OMG! All builds are broken!";
	            
                var description = "";
	            if (numberOfWorkingBuilds > 0)
	                description = numberOfWorkingBuilds + " working";
	            if (numberOfBrokenBuilds > 0)
	                description += ", " + numberOfBrokenBuilds + " broken";
	            if (numberOfUnknownBuilds > 0)
	            {
	                description += ", " + numberOfUnknownBuilds + " unknown";
	            }

	            description += " builds";
	            return description;
	        } 
        }
	}
}

