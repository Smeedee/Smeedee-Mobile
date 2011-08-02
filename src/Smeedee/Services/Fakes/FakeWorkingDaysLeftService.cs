using System;
using System.Linq;
using Smeedee.Model;

namespace Smeedee.Services.Fakes
{
    public class FakeWorkingDaysLeftService : IWorkingDaysLeftService
    {
        private readonly IBackgroundWorker bgWorker;
        private SmeedeeApp app = SmeedeeApp.Instance;
        
        public int DaysLeft = -1;
        public DateTime UntilDate = new DateTime(2011, 7, 15);

        public bool ShouldFail = false;
        
        public FakeWorkingDaysLeftService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }
        
        private string GetDataFromHttp()
        {
            var data = new [] { new [] {DaysLeft.ToString(), UntilDate.ToString()}};
            return Csv.ToCsv(data);
        }

        private void GetSync(Action<int, DateTime> callback, Action failureCallback)
        {
            if (ShouldFail)
            {
                failureCallback();
                return;
            }
            var httpData = GetDataFromHttp();
            var data = Csv.FromCsv(httpData).First();
            callback(int.Parse(data[0]), DateTime.Parse(data[1]));
        }

        public void Get(Action<int, DateTime> callback, Action failureCallback)
        {
            bgWorker.Invoke(() => GetSync(callback, failureCallback));
        }
    }
}
