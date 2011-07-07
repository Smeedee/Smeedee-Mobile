using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters
    {
        public TopCommitters(IEnumerable<Committer> committers)
        {
            Committers = new List<Committer>(committers);
        }
        
        public List<Committer> Committers {
            get;
            private set;
        }
    }
}
