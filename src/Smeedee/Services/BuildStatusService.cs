using System;
using System.Collections.Generic;
using System.Globalization;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class BuildStatusService : IBuildStatusService
    {
        private const int INDEX_BUILD_NAME = 0;
        private const int INDEX_BUILD_TRIGGER_USERNAME = 1;
        private const int INDEX_BUILD_STATUS = 2;
        private const int INDEX_BUILD_DATETIME = 3;

        private readonly IFetchHttp downloader;
        private readonly IBackgroundWorker bgWorker;

        public BuildStatusService()
        {
            downloader = SmeedeeApp.Instance.ServiceLocator.Get<IFetchHttp>();
            bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
        }

        public void Load(Action<AsyncResult<IEnumerable<Build>>> callback)
        {
            bgWorker.Invoke(() => callback(new AsyncResult<IEnumerable<Build>>(ParseCsv(downloader.DownloadString(new Login().Url + ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH + ServiceConstants.BUILD_STATUS_SERVICE_URL)))));
        }

        private static IEnumerable<Build> ParseCsv(string csvData)
        {
            var csvStringLines = Csv.FromCsv(csvData);
            var results = new List<Build>();
            foreach (var line in csvStringLines)
            {
                try
                {
                    results.Add(new Build(
                            line[INDEX_BUILD_NAME],
                            (BuildState) Enum.Parse(typeof (BuildState), line[INDEX_BUILD_STATUS]),
                            line[INDEX_BUILD_TRIGGER_USERNAME],
                            DateTime.ParseExact(line[INDEX_BUILD_DATETIME], "yyyyMMddHHmmss", CultureInfo.InvariantCulture)));
                }
                catch { }
            }
            return results;
        }
    }
}