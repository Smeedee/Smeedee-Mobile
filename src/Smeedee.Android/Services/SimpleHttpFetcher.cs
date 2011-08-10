using System;
using System.Net;

using Smeedee.Services;

namespace Smeedee.Android.Services
{
    public class SimpleHttpFetcher : IFetchHttp
    {
        public string DownloadString(string url)
        {
            var client = new WebClient();
            try
            {
                return client.DownloadString(url);
            }
            catch (Exception e)
            {
                return "";
            }
        }
    }
}