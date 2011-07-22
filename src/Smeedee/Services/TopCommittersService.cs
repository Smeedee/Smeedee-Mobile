using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class TopCommittersService : ITopCommittersService
    {
        private readonly IBackgroundWorker bgWorker;
        private readonly IPersistenceService persistence;
        private readonly SmeedeeApp app = SmeedeeApp.Instance;

        public TopCommittersService()
        {
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
            persistence = app.ServiceLocator.Get<IPersistenceService>();
        }

        private string GetDataFromHttp(TimePeriod time)
        {
            var login = new Login();
            var url = login.Url + "/MobileServices/TopCommitters.aspx";
            return new WebClient().DownloadString(url);
        }

        private IEnumerable<Committer> Deserialize(string data)
        {
            return Csv.FromCsv(data).Select(s => new Committer(s[0], int.Parse(s[1]), s[2]));
        }
		
        public void LoadTopCommiters(TimePeriod time, Action<IEnumerable<Committer>> callback)
        {
            bgWorker.Invoke(() => callback(Deserialize(GetDataFromHttp(time))));
        }
    }
}
