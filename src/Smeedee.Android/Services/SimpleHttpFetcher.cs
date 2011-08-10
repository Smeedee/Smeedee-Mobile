using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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