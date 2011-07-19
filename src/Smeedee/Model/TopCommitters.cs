using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters
    {
        private readonly ITopCommittersService service = SmeedeeApp.Instance.ServiceLocator.Get<ITopCommittersService>();

        private IEnumerable<Committer> _committers;
        private TimeInterval _timeInterval;
        private int _numberOfCommitters;

        public TopCommitters()
        {
            _committers = new List<Committer>();
            _timeInterval = TimeInterval.PastDay;
            _numberOfCommitters = 5;
        }

        public IEnumerable<Committer> Committers 
        {
            get 
            {
                return _committers.OrderByDescending(e => e.Commits).Take(GetNumberOfCommitters()); 
            }
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

        public string Description
        {
            get
            {
                var interval = GetTimeInterval();
                var suffix = (interval == TimeInterval.PastDay) ? "24 hours" : (interval == TimeInterval.PastWeek) ? "week" : "month";
                return string.Format("Top committers for the past {0}", suffix);
            }
        }

        public enum TimeInterval
        {
            PastDay = 1, PastWeek = 7, PastMonth = 30
        }
    }
}
