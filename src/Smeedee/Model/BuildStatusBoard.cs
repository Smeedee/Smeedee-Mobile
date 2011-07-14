using System.Collections.Generic;

namespace Smeedee.Model
{
	public class BuildStatusBoard : IModel
	{
		public IEnumerable<BuildStatus> Builds { get; private set; }
		
		public BuildStatusBoard (IEnumerable<BuildStatus> builds)
		{
			Builds = builds;
		}
	}
}

