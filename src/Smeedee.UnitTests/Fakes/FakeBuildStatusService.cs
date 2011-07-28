using System;
using System.Collections.Generic;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Fakes
{
    class FakeBuildStatusService : IBuildStatusService
    {
        public IEnumerable<Build> Builds;
        public IEnumerable<Build> DefaultBuilds = new List<Build>()
            {
            new Build("Smeedee-Mobile - Master", BuildState.Working, "dagolap",   DateTime.Now),
            new Build("Smeedee-Mobile - Dev",    BuildState.Broken,  "dagolap",   DateTime.Now + TimeSpan.FromDays(1)),
            new Build("Smeedee - Master",        BuildState.Working, "stavanger", DateTime.Now + TimeSpan.FromDays(2)), 
            new Build("Smeedee - Dev",           BuildState.Unknown, "stavanger", DateTime.Now + TimeSpan.FromDays(3)),
            new Build("SharedMobileTest1",       BuildState.Unknown, "trondheim", DateTime.Now + TimeSpan.FromDays(4)),
            new Build("SharedMobileTest2",       BuildState.Unknown, "trondheim", DateTime.Now + TimeSpan.FromDays(5)),
            new Build("Smeedee Webpage",         BuildState.Working, "stavanger", DateTime.Now + TimeSpan.FromDays(6)),
            new Build("Alex Project",            BuildState.Broken,  "alex",      DateTime.Now + TimeSpan.FromDays(7))
            };


        private readonly IBackgroundWorker bgWorker;
        public FakeBuildStatusService(IBackgroundWorker bgWorker)
        {
            this.bgWorker = bgWorker;
            Builds = DefaultBuilds;
        }

        public void Load(Action<IEnumerable<Build>> callback)
        {
            bgWorker.Invoke(() => callback(Builds));
        }
    }
}
