using System;
using System.Collections.Generic;

namespace Smeedee.Model
{
    public class WorkingDaysLeft
    {
        private int days;
        private IWorkingDaysLeftService service;
        private SmeedeeApp app = SmeedeeApp.Instance;

        public WorkingDaysLeft()
        {
            service = app.ServiceLocator.Get<IWorkingDaysLeftService>();
        }

        public bool LoadError { get; private set; }
        
        public DateTime UntillDate { get; private set; }
		
        public bool IsOnOvertime { get { return days < 0; } }
		
        public int DaysLeft
        {
            get { return Math.Abs(days); }
            private set { days = value; }
        }

        public void Load(Action callback)
        {
            Action handleFailure = () =>
            {
                LoadError = true;
                callback();
            };
            Action<int, DateTime> updateModel = (days, untilDate) =>
            {
                LoadError = false;
                this.days = days;
                this.UntillDate = untilDate;
                callback();
            };
            service.Get(updateModel, handleFailure);
        }

        public string DaysLeftText
        {
            get
            {
                if (days < 0)
                {
                    return days == -1 ? "day on overtime" : "days on overtime";
                }
                return days == 1 ? "working day left" : "working days left";
            }
        }
    }
}
