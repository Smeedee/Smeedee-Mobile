using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class WorkingDaysLeftService : IWorkingDaysLeftService
    {
        private readonly IBackgroundWorker bgWorker;
        private readonly IFetchHttp http;
        private SmeedeeApp app = SmeedeeApp.Instance;
        
        public int DaysLeft;
        public DateTime UntilDate;

        public WorkingDaysLeftService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
            http = app.ServiceLocator.Get<IFetchHttp>();
        }
        
        private string GetDataFromHttp()
        {
            var login = new Login();
            var url = login.Url +
                      ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH +
                      ServiceConstants.WORKING_DAYS_LEFT_SERVICE_URL +
                      "?apiKey=" + login.Key;
			return http.DownloadString(url);
        }

        private void GetSync(Action<int, DateTime> callback, Action failureCallback)
        {
            var httpData = GetDataFromHttp();
            var data = Csv.FromCsv(httpData).FirstOrDefault();
			
            if (data == null || data.Count() != 2)
            {
                failureCallback();
                return;
            }
			
            try
            {
                callback(int.Parse(data[0]), DateTime.ParseExact(data[1], "yyyyMMddHHmmss", CultureInfo.InvariantCulture));
            } 
			catch (FormatException)
            {
                failureCallback();
            }
        }

        public void Get(Action<int, DateTime> callback, Action failureCallback)
        {
            bgWorker.Invoke(() => GetSync(callback, failureCallback));
        }
    }
}
