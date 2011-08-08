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
		private IBackgroundWorker worker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
			Console.WriteLine("Fetching image " + uri);
			
			worker.Invoke(() => {
				byte[] data = null;
				var client = new WebClient();
				try 
				{
					data = client.DownloadData(uri);
					Console.WriteLine("Returned: " + data);
				} 
				catch (WebException e) 
				{
					Console.WriteLine("Error: " + e.Message);
				}
				callback(data);
			});
        }
    }
}
