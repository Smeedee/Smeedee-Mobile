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

        public int DaysLeft { get; private set; }

        public string DaysLeftText
        {
            get
            {
                if (DaysLeft >= 0)
                {
                    return DaysLeft == 1 ? "working day left" : "working days left";

                }
                else
                {
                    return DaysLeft == -1 ? "day on overtime" : "days on overtime";
                }
            }
        }
    }
}
