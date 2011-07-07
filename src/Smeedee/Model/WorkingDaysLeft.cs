using System;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class WorkingDaysLeft : IModel
    {
        public WorkingDaysLeft(int days)
        {
            if (days < 0)
            {
                throw new ArgumentException("Days left must be a positive integer");
            }
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
