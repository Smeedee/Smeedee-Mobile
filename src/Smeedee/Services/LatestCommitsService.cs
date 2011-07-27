using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Android.Util;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class LatestCommitsService : ILatestCommitsService
    {
        private const int INDEX_COMMIT_MESSAGE = 0;
        private const int INDEX_COMMIT_DATETIME = 1;
        private const int INDEX_COMMIT_USER = 2;
        private const int INDEX_COMMIT_REVISION = 3;

        private readonly IFetchHttp downloader;
        private readonly IBackgroundWorker bgWorker;
        private SmeedeeApp app = SmeedeeApp.Instance;

        public LatestCommitsService()
        {
            downloader = app.ServiceLocator.Get<IFetchHttp>();
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }

        public string GetFromHttp(int revision)
        {
            var parameter = (revision == -1 ? "" : "?revision=" + revision);
            var url = new Login().Url + 
                      ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH +
                      ServiceConstants.LATEST_COMMITS_SERVICE_URL +
                      parameter;
            return downloader.DownloadString(url);
        }

        public void Get10AfterRevision(int fromRevision, Action<IEnumerable<Commit>> callback)
        {
            bgWorker.Invoke(() =>
                callback(ParseCsv(GetFromHttp(fromRevision))));
        }

        public void Get10Latest(Action<IEnumerable<Commit>> callback)
        {
            bgWorker.Invoke(() =>
                callback(ParseCsv(GetFromHttp(-1))));
        }

        private IEnumerable<Commit> ParseCsv(string csvData)
        {
            var csvStringLines = Csv.FromCsv(csvData);
            var results = new List<Commit>();
            foreach (var line in csvStringLines)
            {
                try
                {
                    results.Add(new Commit(
                                    line[INDEX_COMMIT_MESSAGE],
                                    DateTime.Parse(line[INDEX_COMMIT_DATETIME]),
                                    line[INDEX_COMMIT_USER],
                                    int.Parse(line[INDEX_COMMIT_REVISION])));
                }
                catch (FormatException) { }
                catch (IndexOutOfRangeException) { }
            }
            Log.Debug("Smeedee", "Latest commits count: " + /*People matter, */results.Count());
            return results;
        }
    }
}
