using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class FakeBuildStatusService : IModelService<BuildStatus>
    {
        private IList<BuildStatus> builds;
        public FakeBuildStatusService()
        {
            builds = new List<BuildStatus>()
                         {
                             new BuildStatus("Smeedee-Mobile - Master", BuildSuccessState.Success, "dagolap", DateTime.Now),
                             new BuildStatus("Smeedee-Mobile - Dev", BuildSuccessState.Success, "dagolap", DateTime.Now),
                             new BuildStatus("Smeedee - Master", BuildSuccessState.Success, "stavanger", DateTime.Now), 
                             new BuildStatus("Smeedee - Dev", BuildSuccessState.Failure, "stavanger", DateTime.Now)
                         };
        }
        public IEnumerable<BuildStatus> Get()
        {
            return builds;
        }

        public BuildStatus GetSingle()
        {
            return builds[0];
        }
    }
}
