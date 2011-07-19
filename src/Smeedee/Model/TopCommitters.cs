using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters
    {
        private readonly IPersistenceService persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
        private readonly ITopCommittersService service = SmeedeeApp.Instance.ServiceLocator.Get<ITopCommittersService>();

        private IEnumerable<Committer> _committers;

        public TopCommitters()
        {
            _committers = new List<Committer>();
        }

        public void Load(Action callback)
        {
            service.LoadTopCommiters(
                TimePeriod,
                (committers) => { 
                    _committers = committers;
                    callback();
                }
            );
        }

        public IEnumerable<Committer> Committers 
        {
            get { return _committers.OrderByDescending(e => e.Commits).Take(NumberOfCommitters); }
        }

        public int NumberOfCommitters
        {
            get { return int.Parse(persistence.Get("TopCommitters.NumberOfCommitters", "5")); }
            set { persistence.Save("TopCommitters.NumberOfCommitters", value.ToString()); }
        }

        // TODO: Better naming
        public TimePeriod TimePeriod
        {
            get
            {
                var stored = persistence.Get("TopCommitters.TimePeriod", TimePeriod.PastDay.ToString());
                return (TimePeriod) Enum.Parse(typeof (TimePeriod), stored);
            }
            set { persistence.Save("TopCommitters.TimePeriod", value.ToString()); }
        }

        public string Description
        {
            get
            {
                var time = TimePeriod;
                var suffix = (time == TimePeriod.PastDay) ? "24 hours" : (time == TimePeriod.PastWeek) ? "week" : "month";
                return string.Format("Top committers for the past {0}", suffix);
            }
        }
    }

    public enum TimePeriod
    {
        PastDay = 1, PastWeek = 7, PastMonth = 30
    }
}
