using System;
using System.Collections.Generic;

using Smeedee.Model;

namespace Smeedee
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

