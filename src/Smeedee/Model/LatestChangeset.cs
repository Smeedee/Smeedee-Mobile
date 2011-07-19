using System;
using System.Collections.Generic;
using Smeedee;

namespace Smeedee.Model
{
    public class LatestChangesets
    {
        private IEnumerable<Changeset> changesets;
        public IEnumerable<Changeset> Changesets
        {
            get { return changesets; }
            private set { changesets = value; }
        }

        private SmeedeeApp app = SmeedeeApp.Instance;
        private ILatestChangesetsService service;

        public LatestChangesets()
        {
            service = app.ServiceLocator.Get<ILatestChangesetsService>();
        }

        public void Load(Action callback)
        {
            service.Get(changesets =>
            {
                this.changesets = changesets;
                callback();
            });
        }
    }
}