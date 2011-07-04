using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommiters
    {
        private ISmeedeeService smeedeeService = SmeedeeApp.SmeedeeService;
        
        public TopCommiters()
        {
            Commiters = new List<Commiter>();
        }
        
        public List<Commiter> Commiters {
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
