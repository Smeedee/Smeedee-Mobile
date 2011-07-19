using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters : IModel
    {
        private ITopCommittersService service = SmeedeeApp.Instance.ServiceLocator.Get<ITopCommittersService>();

        private IEnumerable<Committer> _committers;
        private TimeInterval _timeInterval = TimeInterval.PastDay;
        private int _numberOfCommitters = 5;
        private int _days;

        public TopCommitters()
        {
            _committers = new List<Committer>();

            /*Committers.Sort(
                (e1, e2) => e2.Commits.CompareTo(e1.Commits)
            );*/
        }

        public IEnumerable<Committer> Committers {
            get { return _committers.OrderBy(e => e.Commits).Take(GetNumberOfCommitters()); }
        }

        public void Load(Action callback)
        {
            service.LoadTopCommiters(
                GetTimeInterval(),
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

        public void SetNumberOfCommitters(int n)
        {
            _numberOfCommitters = n;
        }

        private int GetNumberOfCommitters()
        {
            return _numberOfCommitters;
        }

        public TimeInterval GetTimeInterval()
        {
            return _timeInterval;
        }

        public void SetTimeInterval(TimeInterval t)
        {
            _timeInterval = t;
        }




        public enum TimeInterval
        {
            PastDay = 1, PastWeek = 7, PastMonth = 30
        }
    }
}
