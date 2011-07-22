using System;
using Smeedee;

namespace Smeedee.Model
{
    public class Committer
    {
        public Committer(string name, int commits, string url)
        {
            Guard.NotNull(name);
            Name = name;
            Commits = commits;
            try
            {
                ImageUri = new Uri(url);
            } catch (Exception e)
            {
                ImageUri = new Uri("http://www.foo.com/img.png"); //TODO! <- dont do this
            }
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
