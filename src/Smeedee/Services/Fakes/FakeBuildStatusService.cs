﻿using System;
using System.Collections.Generic;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee
{
    public class FakeBuildStatusService : IBuildStatusService
    {
        private readonly IEnumerable<Build> builds;
        private readonly IEnumerable<Build> builds2;
        private readonly IBackgroundWorker bgWorker;
        private bool buildListSwitch;
        public FakeBuildStatusService(IBackgroundWorker bgWorker)
        {
            this.bgWorker = bgWorker;
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

            builds2 = new List<Build>()
                         {
                            new Build("Smeedee-Mobile - Master", BuildState.Working, "dagolap", DateTime.Now),
                            new Build("Smeedee-Mobile - Dev", BuildState.Broken, "dagolap", DateTime.Now),
                            new Build("Smeedee - Master", BuildState.Working, "stavanger", DateTime.Now), 
                            new Build("Smeedee - Dev", BuildState.Unknown, "stavanger", DateTime.Now)
                         };
        }

        public void Load(Action<AsyncResult<IEnumerable<Build>>> callback)
        {
            if (buildListSwitch)
                bgWorker.Invoke(() => callback(new AsyncResult<IEnumerable<Build>>(builds)));
            else
                bgWorker.Invoke(() => callback(new AsyncResult<IEnumerable<Build>>(builds2)));

            buildListSwitch = !buildListSwitch;
        }
    }
}