using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public int DaysLeft { get; set; }
    }
}
