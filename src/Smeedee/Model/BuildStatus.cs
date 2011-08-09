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

        private readonly IBuildStatusService _buildService = SmeedeeApp.Instance.ServiceLocator.Get<IBuildStatusService>();
	    private readonly IPersistenceService _persistenceService = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();

        private List<Build> _builds;

        public BuildStatus()
        {
            _builds = new List<Build>();
        }

        public void Load(Action callback)
        {
            _buildService.Load(args =>
            {
                Builds = args.ToList();
                callback();
            });
        }

        public BuildOrder Ordering { 
            get
            {
                var orderValue = _persistenceService.Get(SortingPropertyKey, "buildtime");
                return orderValue == "buildname" ? BuildOrder.BuildName : BuildOrder.BuildTime;
            } 
            set
            {
                switch (value)
                {
                    case BuildOrder.BuildName:
                        _persistenceService.Save(SortingPropertyKey, "buildname");
                        break;
                    case BuildOrder.BuildTime:
                        _persistenceService.Save(SortingPropertyKey, "buildtime");
                        break;
                }
            }
        }

        public bool BrokenBuildsAtTop { 
            get 
            { 
                var brokenAtTop = _persistenceService.Get(BrokenFirstPropertyKey, true);
                return brokenAtTop;
            } set
            {
                _persistenceService.Save(BrokenFirstPropertyKey, value);
            }
        }
	    
        public List<Build> Builds { 
            get
            {
                var comparer = (Ordering == BuildOrder.BuildName) ? (IComparer<Build>)new BuildNameComparer() : new BuildTimeComparer();
                return BrokenBuildsAtTop ? GetOrderedBuildsWithBrokenFirst(comparer) : GetOrderedBuilds(comparer);
            } 
            private set
            {
                _builds = value;
            } 
        }

	    public int GetNumberOfBuildsByState(BuildState successState)
	    {
            return _builds.Where(build => build.BuildSuccessState == successState).Count();
	    }

        private List<Build> GetOrderedBuilds(IComparer<Build> comparer)
        {
            return _builds.OrderBy(b => b, comparer).ToList();
        }

        private List<Build> GetOrderedBuildsWithBrokenFirst(IComparer<Build> comparer)
        {
            var brokenBuilds = _builds.Where(b => b.BuildSuccessState == BuildState.Broken).OrderBy(b => b, comparer).ToList();
            var workingBuilds = _builds.Where(b => b.BuildSuccessState == BuildState.Working).OrderBy(b => b, comparer).ToList();
            var unknownBuilds = _builds.Where(b => b.BuildSuccessState == BuildState.Unknown).OrderBy(b => b, comparer).ToList();

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
	            var numberOfBuilds = _builds.Count();


                if (numberOfBuilds <= 0)
                    return "No builds fetched from the Smeedee Server";
	            if (numberOfWorkingBuilds == 0 && numberOfUnknownBuilds == 0)
	                return "OMG! All builds are broken!";
                var description = "";
	            if (numberOfWorkingBuilds > 0)
	                description = numberOfWorkingBuilds + " working";
	            if (numberOfBrokenBuilds > 0)
	                description += (description == "") ? numberOfBrokenBuilds + " broken" : ", " + numberOfBrokenBuilds + " broken";
	            if (numberOfUnknownBuilds > 0)
	            {
	                description += (description == "") ? numberOfUnknownBuilds + " unknown" : ", " + numberOfUnknownBuilds + " unknown";
	            }

	            description += " builds";
	            return description;
	        } 
        }
	}
}