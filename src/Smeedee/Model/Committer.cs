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
                //Use a special uri to tell the image cache to load the default image from disk.
                //TODO: Actually implement this in the image cache
                ImageUri = new Uri("smeedee://default_person.png");
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
