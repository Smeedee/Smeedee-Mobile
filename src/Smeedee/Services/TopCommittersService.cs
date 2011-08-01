using System;
using System.Collections.Generic;
using System.Linq;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class TopCommittersService : ITopCommittersService
    {
        private readonly SmeedeeApp app = SmeedeeApp.Instance;
        private readonly IBackgroundWorker bgWorker;
        private readonly IFetchHttp http;

        public TopCommittersService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
            http = app.ServiceLocator.Get<IFetchHttp>();
        }

        private string GetDataFromHttp(TimePeriod time)
        {
            var login = new Login();
            var url = login.Url +
                      ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH +
                      ServiceConstants.TOP_COMMITTERS_SERVICE_URL +
                      "?days=" + (int)time +
                      "&apiKey=" + login.Key;
            return http.DownloadString(url);
        }

        private IEnumerable<Committer> Deserialize(string data)
        {
            var csv = Csv.FromCsv(data);
            return csv.Where(s => s.Length == 3).Select(s => new Committer(s[0], int.Parse(s[1]), s[2]));
        }
		
        public void LoadTopCommiters(TimePeriod time, Action<IEnumerable<Committer>> callback)
        {
            bgWorker.Invoke(() => callback(Deserialize(GetDataFromHttp(time))));
        }
    }
}
