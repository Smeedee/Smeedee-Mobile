using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class Committer
    {
        public Committer(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            
            Name = name;
        }
        
        public string Name {
            get;
            private set;
        }
    }
}
