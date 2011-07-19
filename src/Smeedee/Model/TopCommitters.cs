using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Smeedee.Model
{
    public class TopCommitters : IModel
    {
        private ITopCommittersService service = SmeedeeApp.Instance.ServiceLocator.Get<ITopCommittersService>();

        private IEnumerable<Committer> _committers;
        private int _days;

        public TopCommitters()
        {
            _committers = new List<Committer>();

            /*Committers.Sort(
                (e1, e2) => e2.Commits.CompareTo(e1.Commits)
            );*/
        }

        public IEnumerable<Committer> Committers {
            get { return _committers.Take(5); }
        }

        public void Load(Action callback)
        {
            service.LoadTopCommiters(
                (committers) => { 
                    _committers = committers;
                    callback();
                }
            );
        }


        public string DaysText
        {
            get
            {
                string suffix;
                if (_days == 1) suffix = "24 hours";
                else if (_days == 7) suffix = "week";
                else if (_days == 30) suffix = "month";
                else suffix = string.Format("{0} days", _days);
                return string.Format("Top committers for the past {0}", suffix);
            }
        }
    }
}
