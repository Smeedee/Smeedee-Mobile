using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class TopCommitters
    {
        public static readonly string TimePeriodPropertyKey = "TopCommitters.TimePeriod";
        public static readonly string NumberOfCommittersPropertyKey = "TopCommitters.NumberOfCommitters";

        private static readonly string DefaultNumberOfCommittersPropertyValue = "5";
        private static readonly string DefaultTimePeriodPropertyValue = TimePeriod.PastDay.ToString();

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
            get
            {
                var stored = persistence.Get(NumberOfCommittersPropertyKey, DefaultNumberOfCommittersPropertyValue);
                return int.Parse(stored);
            }
            set { persistence.Save(NumberOfCommittersPropertyKey, value.ToString()); }
        }

        public TimePeriod TimePeriod
        {
            get
            {
                var stored = persistence.Get(TimePeriodPropertyKey, DefaultTimePeriodPropertyValue);
                return (TimePeriod) Enum.Parse(typeof (TimePeriod), stored);
            }
            set { persistence.Save(TimePeriodPropertyKey, value.ToString()); }
        }

        public string Description
        {
            get { return string.Format("Top committers for the past {0}", TimePeriod.ToSuffix()); }
        }
    }

    // TODO: Better naming
    public enum TimePeriod
    {
        PastDay = 1, PastWeek = 7, PastMonth = 30
    }

    public static class TimePeriodExtensions
    {
        public static string ToSuffix(this TimePeriod time)
        {
            return (time == TimePeriod.PastDay) ? "24 hours" : (time == TimePeriod.PastWeek) ? "week" : "month";
        }
    }
}
