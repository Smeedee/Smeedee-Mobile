using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Model;

namespace Smeedee
{
    public class WorkingDaysLeftFakeService : IWorkingDaysLeftService
    {
        private readonly IBackgroundWorker bgWorker;
        private SmeedeeApp app = SmeedeeApp.Instance;
        
        public int DaysLeft = -1;
        public DateTime UntilDate = new DateTime(2011, 7, 15);
        
        public WorkingDaysLeftFakeService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }
        
        private void GetSync(Action<int, DateTime> callback)
        {
            callback(DaysLeft, UntilDate);
        }

        public void Get(Action<int, DateTime> callback)
        {
            bgWorker.Invoke(() => GetSync(callback));
        }
    }
}
