using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class LatestCommits
    {
        public const string HighlightEmptyPropertyKey = "LatestCommits.HighlightEmpty";
		
        private List<Commit> commits = new List<Commit>();
        public List<Commit> Commits
        {
            get { return commits; }
            private set { commits = value; }
        }
		
        public string DynamicDescription
        {
            get { return "Latest " + Commits.Count() + " commits"; }
        }

        private ILatestCommitsService service;
		private IPersistenceService persistence;

        public LatestCommits()
        {
            service = SmeedeeApp.Instance.ServiceLocator.Get<ILatestCommitsService>();
			persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
        }

        public void Load(Action callback)
        {
            service.Get10Latest(commits =>
                {
                    this.commits = commits.ToList();
                    callback();
                });
        }

        public void LoadMore(Action callback)
        {
            var fromRevision = Commits.Last().Revision;
            service.Get10FromRevision(fromRevision, loadedCommits =>
            {
                StoreNewCommits(loadedCommits);
                callback();
            });
        }

        private void StoreNewCommits(IEnumerable<Commit> newCommits)
        {
            //This has quadratic runtime, O(commits.Count * newlist.Count), 
            //but thats ok as long as newlist.Count is limited to 10
            foreach (var commit in newCommits)
                if (!Enumerable.Contains(commits, commit))
                    commits.Add(commit);
        }
		
		public bool HighlightEmpty
		{
			get { return true; }
			set { persistence.Save(HighlightEmptyPropertyKey, value); }
		}
    }
}