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
		private ILog logger = SmeedeeApp.Instance.ServiceLocator.Get<ILog>();
		private IBackgroundWorker worker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
			logger.Log("[Fetching]", uri.ToString());
			
			worker.Invoke(() => {
				byte[] data = null;
				var client = new WebClient();
				try 
				{
					data = client.DownloadData(uri);
					logger.Log("[Image]", data.ToString());
				} 
				catch (WebException e) 
				{
					logger.Log("Exception", e.Message);
				}
				callback(data);
			});
        }
    }
}
