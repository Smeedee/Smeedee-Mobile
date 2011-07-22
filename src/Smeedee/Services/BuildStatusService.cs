﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class BuildStatusService : IBuildStatusService
    {
        private IFetchHtml downloader;
        private IBackgroundWorker bgWorker;
        private IPersistenceService persistenceService;

        public BuildStatusService()
        {
            downloader = SmeedeeApp.Instance.ServiceLocator.Get<IFetchHtml>();
            bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
            persistenceService = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
        }

        public void Load(Action<AsyncResult<IEnumerable<Build>>> callback)
        {
            bgWorker.Invoke(() =>
                                {
                                    var url = persistenceService.Get(Login.LoginUrl,
                                                                     "http://services.smeedee.org/smeedee/");
                                    callback(new AsyncResult<IEnumerable<Build>>(ParseCsv(downloader.DownloadString(url + "/MobileServices/BuildStatus.aspx"))));
                                });
        }

        private static IEnumerable<Build> ParseCsv(string downloadString)
        {
            return Csv.FromCsv(downloadString).Select(s => new Build(
                s[0], 
                (BuildState)Enum.Parse(typeof(BuildState), s[2]), 
                s[1],
                DateTime.ParseExact(s[3], "yyyyMMddHHmmss", CultureInfo.InvariantCulture)));

        }
    }
}
