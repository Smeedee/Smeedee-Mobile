using System;
using System.Collections.Generic;
using Smeedee;

namespace Smeedee.Model
{
    public class LatestCommits
    {
        private IEnumerable<Commit> commits = new List<Commit>();
        public IEnumerable<Commit> Commits
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
            service.Get(commits =>
            {
                this.commits = commits;
                callback();
            });
        }
    }
}