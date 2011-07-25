using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class LatestCommitsService : ILatestCommitsService
    {
        private const int INDEX_COMMIT_MESSAGE = 0;
        private const int INDEX_COMMIT_DATETIME = 1;
        private const int INDEX_COMMIT_USER = 2;

        private readonly IFetchHttp downloader;
        private readonly IBackgroundWorker bgWorker;
        private readonly IPersistenceService persistenceService;

        public LatestCommitsService()
        {
            downloader = SmeedeeApp.Instance.ServiceLocator.Get<IFetchHttp>();
            bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
            persistenceService = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
        }

        public void Get10(int fromIndex, Action<IEnumerable<Commit>> callback)
        {
            bgWorker.Invoke(() =>
            {
                var url = persistenceService.Get(Login.LoginUrl, "http://services.smeedee.org/smeedee/");
                if (!url.EndsWith("/")) url += "/";
                callback(ParseCsv(downloader.DownloadString(url + ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH + ServiceConstants.LATEST_COMMITS_SERVICE_URL + "?fromRevision=" + fromIndex)));
            });
        }

        private static IEnumerable<Commit> ParseCsv(string csvData)
        {
            var csvStringLines = Csv.FromCsv(csvData);
            var results = new List<Commit>();
            foreach (var line in csvStringLines)
            {
                try
                {
                    results.Add(new Commit(
                                    line[INDEX_COMMIT_MESSAGE],
                                    DateTime.ParseExact(line[INDEX_COMMIT_DATETIME], ServiceConstants.DATETIME_STRING_FORMAT, CultureInfo.InvariantCulture),
                                    line[INDEX_COMMIT_USER]));
                }
                catch { }
            }
            return results;
        }
    }
}
