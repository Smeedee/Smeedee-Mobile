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
            Commiters = new List<Committer>();
        }
        
        public List<Committer> Commiters {
            get;
            private set;
        }
        
        public void Load(Action callback)
        {
            smeedeeService.LoadTopCommiters((args) => {
                Commiters.AddRange(args.Result);
                callback();
            });
        }
    }
}
