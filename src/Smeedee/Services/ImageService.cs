using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using Smeedee;
using Smeedee.Lib;
using Smeedee.Model;

namespace Smeedee
{
    // Usage Android:
    // var img = new ImageView();
    // var bmp = new BitmapFactory().decodeByteArray(bytes);
    // img.setImageBitmap(bmp);
    
    // Usage iPhone:
    // UIImage img = UIImage.LoadFromData(NSData.FromArray(bytes));
    
    public class ImageService : IImageService
    {
        private readonly TimeSpan TIMEOUT = TimeSpan.FromSeconds(15);

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
			Console.WriteLine("Fetching image " + uri);
			// WebClient is not thread-safe, need new instance for each thread
            new Thread(() => {
				byte[] data = null;
                try
                {
                    var manualReset = new ManualResetEvent(false);
                    var client = new WebClient();

                    client.OpenReadCompleted += (o, e) => {if (e.Error == null) 
						{
                            data = e.Result.ReadToEnd();
						}
						else {
							Console.WriteLine("Error: " + e.Error);
						}
                        manualReset.Set();
                    };
                    client.OpenReadAsync(uri);

                    manualReset.WaitOne(TIMEOUT);
				} 
				catch (WebException e) 
				{
					Console.WriteLine("Image error: " + e.Message);
                    //Do nothing, call callback with null as argument
				}
				
				Console.WriteLine("Returning image data: " + data);
				
				callback(data);
            }).Start();
        }
    }
}
