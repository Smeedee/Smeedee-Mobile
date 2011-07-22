using System;
using System.Net;

namespace Smeedee.Services
{
    public class HttpFetcher : IFetchHttp
    {
        public string DownloadString(string url)
        {
            var client = new WebClient();
            try
            {
                return client.DownloadString(url);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
