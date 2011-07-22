using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class BuildStatusService : IBuildStatusService
    {
        private IFetchHtml downloader;
        private IBackgroundWorker bgWorker;

        public BuildStatusService(IFetchHtml webDownloader, IBackgroundWorker backgroundWorker)
        {
            downloader = webDownloader;
            bgWorker = backgroundWorker;
        }

        public void Load(Action<AsyncResult<IEnumerable<Build>>> callback)
        {
            bgWorker.Invoke(() => callback(new AsyncResult<IEnumerable<Build>>(ParseCsv(downloader.DownloadString()))));
        }

        private static IEnumerable<Build> ParseCsv(string downloadString)
        {
            return Csv.FromCsv(downloadString).Select(s => new Build(s[0], (BuildState)Enum.Parse(typeof(BuildState), s[2]), s[1], DateTime.Parse(s[3])));
        }
    }
}
