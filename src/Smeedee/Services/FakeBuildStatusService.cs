using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class FakeBuildStatusService : IModelService<BuildStatusBoard>
    {
        private IList<BuildStatus> builds;
        public FakeBuildStatusService()
        {
            builds = new List<BuildStatus>()
                         {
                             new BuildStatus("Smeedee-Mobile - Master", BuildSuccessState.Success, "dagolap", DateTime.Now),
                             new BuildStatus("Smeedee-Mobile - Dev", BuildSuccessState.Failure, "dagolap", DateTime.Now),
                             new BuildStatus("Smeedee - Master", BuildSuccessState.Success, "stavanger", DateTime.Now), 
                             new BuildStatus("Smeedee - Dev", BuildSuccessState.Unknown, "stavanger", DateTime.Now),
                             new BuildStatus("SharedMobileTest1", BuildSuccessState.Unknown, "trondheim", DateTime.Now),
                             new BuildStatus("SharedMobileTest2", BuildSuccessState.Unknown, "trondheim", DateTime.Now),
                             new BuildStatus("Smeedee Webpage", BuildSuccessState.Success, "stavanger", DateTime.Now),
                             new BuildStatus("Alex Project", BuildSuccessState.Failure, "alex", DateTime.Now)
                         };
        }
		
        public BuildStatusBoard Get()
        {
            return new BuildStatusBoard(builds);
        }
		
        public BuildStatusBoard Get(IDictionary<string, string> args)
        {
            return new BuildStatusBoard(builds);
        }
    }
}
