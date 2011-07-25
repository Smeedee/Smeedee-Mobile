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
        private readonly IPersistenceService persistenceService;

        public BuildStatusService()
        {
            downloader = SmeedeeApp.Instance.ServiceLocator.Get<IFetchHttp>();
            bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
            persistenceService = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
        }

        public void Load(Action<AsyncResult<IEnumerable<Build>>> callback)
        {
            bgWorker.Invoke(() =>
                                {
                                    var url = persistenceService.Get(Login.LoginUrl, "http://services.smeedee.org/smeedee/");
                                    if (!url.EndsWith("/")) url += "/";
                                    callback(new AsyncResult<IEnumerable<Build>>(ParseCsv(downloader.DownloadString(url + ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH + ServiceConstants.BUILD_STATUS_SERVICE_URL))));
                                });
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