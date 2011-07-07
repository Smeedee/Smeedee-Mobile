using System;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class WorkingDaysLeft
    {
        public WorkingDaysLeft(int days)
        {
            DaysLeft = days;
        }

        public int DaysLeft { get; private set; }

        public string DaysLeftText
        {
            get
            {
                return DaysLeft == 1 ? "working day left" : "working days left";
            }
        }
    }
}
