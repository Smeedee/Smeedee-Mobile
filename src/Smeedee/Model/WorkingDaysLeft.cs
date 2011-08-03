using System;

namespace Smeedee.Model
{
    public class WorkingDaysLeft
    {
        private int days;
        private readonly IWorkingDaysLeftService service;

        public WorkingDaysLeft()
        {
            service = SmeedeeApp.Instance.ServiceLocator.Get<IWorkingDaysLeftService>();
        }

        public bool LoadError { get; private set; }

        public DateTime UntillDate { get; private set; }

        public bool IsOnOvertime
        {
            get { return days < 0; }
        }

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
            Action<int, DateTime> updateModel = (days, untillDate) =>
                                                    {
                                                        LoadError = false;
                                                        this.days = days;
                                                        UntillDate = untillDate;
                                                        callback();
                                                    };
            service.Get(updateModel, handleFailure);
        }

        public string DaysLeftText
        {
            get
            {
                if (IsOnOvertime)
                {
                    return days == -1 ? "day on overtime" : "days on overtime";
                }
                return days == 1 ? "working day left" : "working days left";
            }
        }

        public string UntillText
        {
            get
            {
                return IsOnOvertime
                       ? "you should have been finished by " + UntillDate.DayOfWeek + " " +
                         UntillDate.Date.ToShortDateString()
                       : "untill " + UntillDate.DayOfWeek + " " + UntillDate.Date.ToShortDateString();
            } 
        }
    }
}
