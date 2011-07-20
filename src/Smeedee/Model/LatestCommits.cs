using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee;

namespace Smeedee.Model
{
    public class LatestCommits
    {
        private List<Commit> commits = new List<Commit>();
        public List<Commit> Commits
        {
            get { return commits; }
            private set { commits = value; }
        }

        private SmeedeeApp app = SmeedeeApp.Instance;
        private ILatestCommitsService service;

        public LatestCommits()
        {
            service = app.ServiceLocator.Get<ILatestCommitsService>();
        }

        public void Load(Action callback)
        {
            service.Get10(0, commits =>
            {
                this.commits = commits.ToList();
                callback();
            });
        }

        public void LoadMore(Action callback)
        {
            var fromIndex = Commits.Count();
            service.Get10(fromIndex, loadedCommits =>
            {
                StoreNewCommits(loadedCommits);
                callback();
            });
        }

        private void StoreNewCommits(IEnumerable<Commit> newList)
        {
            //This has quadratic runtime, O(commits.Count * newlist.Count), 
            //but thats ok as long as newlist.Count is limited to 10
            foreach (var commit in newList)
                if (!HaveStoredCommit(commit))
                    commits.Add(commit);
        }

        private bool HaveStoredCommit(Commit commit)
        {
            foreach (var storedCommit in commits)
                if (storedCommit.Equals(commit))
                    return true;
            return false;
        }
    }
}