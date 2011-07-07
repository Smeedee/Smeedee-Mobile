using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters
    {
        private readonly ITopCommittersService service = SmeedeeApp.Instance.ServiceLocator.Get<ITopCommittersService>();
        
        public TopCommitters()
        {
            Committers = new List<Committer>();
        }
        
        public List<Committer> Committers {
            get;
            private set;
        }
        
        public void Load(Action callback)
        {
            service.LoadTopCommiters((args) => {
                Committers.AddRange(args);
                callback();
            });
        }
    }
}
