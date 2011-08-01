using System;
using System.Net;
using Android.Util;

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
            catch (Exception e)
            {
                Log.Debug("SMEEDEE", "Exception: " + e.Message);
                return e.Message;
            }
        }
    }
}
