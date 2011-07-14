using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public class FakeBuildStatusService : IModelService<BuildStatus>
    {
        private IEnumerable<Build> builds;
        public FakeBuildStatusService()
        {
            builds = new List<Build>()
                         {
                            new Build("Smeedee-Mobile - Master", BuildState.Working, "dagolap", DateTime.Now),
                            new Build("Smeedee-Mobile - Dev", BuildState.Broken, "dagolap", DateTime.Now),
                            new Build("Smeedee - Master", BuildState.Working, "stavanger", DateTime.Now), 
                            new Build("Smeedee - Dev", BuildState.Unknown, "stavanger", DateTime.Now),
                            new Build("SharedMobileTest1", BuildState.Unknown, "trondheim", DateTime.Now),
                            new Build("SharedMobileTest2", BuildState.Unknown, "trondheim", DateTime.Now),
                            new Build("Smeedee Webpage", BuildState.Working, "stavanger", DateTime.Now),
                            new Build("Alex Project", BuildState.Broken, "alex", DateTime.Now)
                         };
        }
		
        public BuildStatus Get()
        {
            return new BuildStatus(builds);
        }
		
        public BuildStatus Get(IDictionary<string, string> args)
        {
            return new BuildStatus(builds);
        }
    }
}
