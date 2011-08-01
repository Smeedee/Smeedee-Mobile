using System;
using Smeedee;
using Smeedee.Services;

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
                ImageUri = new Uri(DiskCachedImageService.DEFAULT_URI);
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
