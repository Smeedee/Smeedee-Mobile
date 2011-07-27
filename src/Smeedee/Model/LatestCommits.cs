using System;
using System.Collections.Generic;
using System.Linq;

namespace Smeedee.Model
{
    public class LatestCommits
    {
        private ILatestCommitsService service;
        private SmeedeeApp app = SmeedeeApp.Instance;

        public List<Commit> Commits { get; private set; }

        public bool HasMore { get; private set; }

        public string DynamicDescription
        {
            get
            {
                return "Latest " + Commits.Count() + " commits";
            }
        }

        public LatestCommits()
        {
            service = app.ServiceLocator.Get<ILatestCommitsService>();
            Commits = new List<Commit>();
            HasMore = true;
        }

        public void Load(Action callback)
        {
            service.Get10Latest(loadedCommits =>
            {
                if (loadedCommits.Count() < 10) HasMore = false;
                Commits = loadedCommits.ToList();
                callback();
            });
        }
        
        public void LoadMore(Action callback)
        {           
            if (!HasMore || Commits.Count == 0)
            {
                callback();
                return;
            }
            var fromRevision = Commits.Last().Revision;
            service.Get10AfterRevision(fromRevision, loadedCommits =>
            {
                if (loadedCommits.Count() < 10) HasMore = false;
                StoreNewCommits(loadedCommits);
                callback();
            });
        }

        private void StoreNewCommits(IEnumerable<Commit> loadedCommits)
        {
            //This has quadratic runtime, O(commits.Count * newlist.Count), 
            //but thats ok as long as newlist.Count is limited to 10
            foreach (var commit in loadedCommits)
                if (!Enumerable.Contains(Commits, commit))
                    Commits.Add(commit);
        }
    }
}