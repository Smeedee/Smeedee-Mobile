using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters
    {
        public TopCommitters()
        {
            Committers = new List<Committer>();
        }
        
        public List<Committer> Committers {
            get;
            private set;
        }
    }
}
