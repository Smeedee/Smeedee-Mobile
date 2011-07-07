using System;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class WorkingDaysLeft
    {
        private IWorkingDaysLeftService service = SmeedeeApp.Instance.ServiceLocator.Get<IWorkingDaysLeftService>();

        public void Load(Action callback)
        {
            service.GetNumberOfWorkingDaysLeft((result) => { 
                DaysLeft = result;
                callback(); 
            }); 
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
