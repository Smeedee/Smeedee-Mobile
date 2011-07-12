using System;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class WorkingDaysLeft : IModel
    {
        public WorkingDaysLeft(int days)
        {
            DaysLeft = days;
        }

        private int days;

        public int DaysLeft
        {
            get { return Math.Abs(days); }
            private set { days = value; }
        }

        public string DaysLeftText
        {
            get
            {
                if (days >= 0)
                {
                    return days == 1 ? "working day left" : "working days left";

                }
                else
                {
                    return days == -1 ? "day on overtime" : "days on overtime";
                }
            }
        }
    }
}
