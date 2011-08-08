using System;
using System.Net;
using System.Threading;

namespace Smeedee.Services
{
    public class HttpFetcher : IFetchHttp
    {
        /*
         * What we do here is to do force an async WebClient call to be sync.
         * This is for historical reasons. This was originally a synchronous api,
         * (but meant to be called in a background thread).
         * But WP7 doesn't support the sync api, so we have to force the async call to be synchronous.
         * 
         * In summary: We take an async api, make it sync, then fire up a thread to use it asynchronously.
         * And this dance is intentional:)         
         */
        static ManualResetEvent manualReset;
        private readonly TimeSpan TIMEOUT = TimeSpan.FromSeconds(15);

        public string DownloadString(string url)
        {
            Uri uri;
            try
            {
                url += (url.Contains("?") ? "&" : "?") + "nocache=" + new Random().Next();
                uri = new Uri(url);
            } catch (FormatException)
            {
                return "";
            }
            var manualReset = new ManualResetEvent(false);
            var client = new WebClient();

            var result = "";
            client.DownloadStringCompleted += (o, e) => 
            {
                if (e.Error == null)
                    result = e.Result;
                manualReset.Set();
            };
            client.DownloadStringAsync(uri);

            manualReset.WaitOne(TIMEOUT);

            return result;
        }
    }
}
