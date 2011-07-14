using System.Collections.Generic;
using System.Linq;

namespace Smeedee.Model
{
	public class BuildStatus : IModel
	{
		public IEnumerable<Build> Builds { get; private set; }
        public BuildOrder Ordering { get; private set; }
        public bool BrokenBuildsAtTop { get; private set; }
		
        public BuildStatus (IEnumerable<Build> builds)
		{
			Builds = builds;
            Ordering = BuildOrder.BuildTime;
		    BrokenBuildsAtTop = true;
		}
        
        public BuildStatus(IEnumerable<Build> builds, BuildOrder ordering, bool brokenBuildsAtTop)
        {
            Builds = builds;
            Ordering = ordering;
            BrokenBuildsAtTop = brokenBuildsAtTop;
        }

        public int GetNumberOfBuildsThatHaveState(BuildState successState)
	    {
            return Builds.Where(build => build.BuildSuccessState == successState).Count();
	    }
	}
}

