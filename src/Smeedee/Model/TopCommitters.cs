using System.Collections.Generic;

namespace Smeedee.Model
{
    public class TopCommitters : IModel
    {
        public TopCommitters(IEnumerable<Committer> committers, int days)
        {
            Days = days;
            Committers = new List<Committer>(committers);
            Committers.Sort(
                (e1, e2) => e2.Commits.CompareTo(e1.Commits)
            );
        }

        public int Days {
            get; 
            private set;
        }

        public List<Committer> Committers { 
            get;
            private set;
        }
    }
}
