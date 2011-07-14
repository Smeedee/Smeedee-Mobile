using System.Collections.Generic;
using System.Linq;

namespace Smeedee.Model
{
	public class BuildStatus : IModel
	{
		public IEnumerable<Build> Builds { get; private set; }
		
		public BuildStatus (IEnumerable<Build> builds)
		{
			Builds = builds;
		}

        public int GetNumberBuildsThatAre(BuildState successState)
	    {
            return Builds.Where(build => build.BuildSuccessState == successState).Count();
	    }
	}
}

