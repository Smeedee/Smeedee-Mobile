using System;
using Smeedee.Lib;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class Committer
    {
        public string Name { get; private set; }
        public int Commits { get; private set; }
        public Uri ImageUri { get; private set; }

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
        
        
    }
}
