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

        public LatestCommitsService()
        {
            downloader = SmeedeeApp.Instance.ServiceLocator.Get<IFetchHttp>();
            bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
        }

        public void Get10(int fromIndex, Action<IEnumerable<Commit>> callback)
        {
            bgWorker.Invoke(() => 
                callback(ParseCsv(downloader.DownloadString(new Login().Url + ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH + ServiceConstants.LATEST_COMMITS_SERVICE_URL + "?fromRevision=" + fromIndex))));
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
