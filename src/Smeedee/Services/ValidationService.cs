using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Util;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class ValidationService : IValidationService
    {
        private IFetchHttp http;
        private IBackgroundWorker bgWorker;

        public ValidationService()
        {
            http = SmeedeeApp.Instance.ServiceLocator.Get<IFetchHttp>();
            bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
        }

        public void Validate(string url, string key, Action<bool> callback)
        {
            bgWorker.Invoke(() =>
                                {
                                    url += ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH +
                                           ServiceConstants.VALIDATION_SERVICE_URL +
                                           "?apiKey=" + key;
                                    var result = http.DownloadString(url);
                                    callback(result == "True");
                                });
        }
    }
}
