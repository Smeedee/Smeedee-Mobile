using System;
using System.Collections.Generic;

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

        public LatestChangesets()
        {
        }

        public void Load(Action callback)
        {
            callback();
        }
    }
}