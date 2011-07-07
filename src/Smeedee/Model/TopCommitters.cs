using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters : IModel
    {
        public TopCommitters(IEnumerable<Committer> committers)
        {
            Committers = new List<Committer>(committers);
            Committers.Sort(
                (e1, e2) => e2.Commits.CompareTo(e1.Commits)
            );
        }

        public List<Committer> Committers { 
            get;
            private set;
        }
    }
}
