using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Smeedee;
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
        private IBackgroundWorker worker;

        public ImageService()
        {
            this.worker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
        }

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
			// WebClient is not thread-safe, need new instance for each thread
            worker.Invoke(() => {
                var webClient = new WebClient();
				byte[] data;
				
				try {
                	data = webClient.DownloadData(uri);
				} 
				catch (WebException) 
				{
					data = null;
				}
				finally
				{
					webClient.Dispose();
				}
				
				callback(data);
            });
        }
    }
}
