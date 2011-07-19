using System;
using System.Collections.Generic;

namespace Smeedee.Model
{
    public class TopCommitters : IModel
    {
        private int _days;

        public TopCommitters()
        {
            /*Committers.Sort(
                (e1, e2) => e2.Commits.CompareTo(e1.Commits)
            );*/
        }

        public List<Committer> Committers { 
            get;
            private set;
        }

        public void Load(Action callback)
        {
            callback();
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
