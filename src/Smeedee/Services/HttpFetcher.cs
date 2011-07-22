using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Smeedee.Services
{
    class HttpFetcher : IFetchHtml
    {
        public string DownloadString(string url)
        {
            var client = new WebClient();
            return client.DownloadString(url);
        }
    }
}
