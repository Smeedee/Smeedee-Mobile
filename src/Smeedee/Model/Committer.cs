using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class Committer
    {
        public Committer(string name, int commits, string url)
        {
            if (name == null)
            {
                throw new ArgumentNullException("name");
            }
            Name = name;
            Commits = commits;
            ImageUri = new Uri(url);
        }
        
        public string Name {
            get;
            private set;
        }

        public int Commits {
            get; 
            private set;
        }

        public Uri ImageUri
        {
            get;
            private set;
        }
    }
}
