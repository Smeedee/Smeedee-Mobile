using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters
    {
        private readonly ISmeedeeService smeedeeService = SmeedeeApp.Instance.ServiceLocator.Get<ISmeedeeService>();
        
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
            smeedeeService.LoadTopCommiters((args) => {
                Committers.AddRange(args.Result);
                callback();
            });
        }
    }
}
