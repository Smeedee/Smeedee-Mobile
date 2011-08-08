using System;
using System.Net;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Smeedee.Lib;

namespace Smeedee.WP7.Lib
{
    public static class WebClientDownloadDataExtension
    {
        private static readonly TimeSpan TIMEOUT = TimeSpan.FromSeconds(15);

        public static byte[] DownloadData(this WebClient webClient, Uri uri)
        {
            byte[] data = null;
            try
            {
                var manualReset = new ManualResetEvent(false);
                var client = new WebClient();

                client.OpenReadCompleted += (o, e) =>
                {
                    if (e.Error == null)
                    {
                        data = e.Result.ReadToEnd();
                    }
                    manualReset.Set();
                };
                client.OpenReadAsync(uri);

                manualReset.WaitOne(TIMEOUT);
            }
            catch (WebException e)
            {
                //Do nothing, return null
            }
            return data;
        } 
    }
}
