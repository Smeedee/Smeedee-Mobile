using System;

namespace Smeedee.Model
{
    public class WorkingDaysLeft : IModel
    {
        private int days;

        public WorkingDaysLeft(int days, DateTime untillDate)
        {
            DaysLeft = days;
            UntillDate = untillDate;
        }

        public DateTime UntillDate { get; private set; }
		
        public bool IsOnOvertime { get { return days < 0; } }
		
        public int DaysLeft
        {
            get { return Math.Abs(days); }
            private set { days = value; }
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
