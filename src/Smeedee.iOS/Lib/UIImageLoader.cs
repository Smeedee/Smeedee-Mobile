using System;
using System.Collections.Generic;
using System.Net;
using Smeedee.Model;
using Smeedee.Services;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS.Lib
{
	public class UIImageLoader
	{
		private static UIImage defaultImage = UIImage.FromFile("images/default_person.jpeg");
		private readonly IImageService service = SmeedeeApp.Instance.ServiceLocator.Get<IImageService>();
		
		public UIImageLoader() { }
		
		public void LoadImageFromUri(Uri uri, Action<UIImage> callback) 
		{
			service.GetImage(uri, (bytes) => {
				using (var pool = new NSAutoreleasePool())
				{
					var ret = (bytes != null) ? UIImage.LoadFromData(NSData.FromArray(bytes)) : defaultImage;
					callback(ret);
				}
			});
		}
	}
	
	internal class TrivialImageService : IImageService
	{
		private IBackgroundWorker worker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
		
		public void GetImage(Uri uri, Action<byte[]> callback)
		{
			// WebClient is not thread-safe, need new instance for each thread
            worker.Invoke(() => {
				byte[] data = null;
                try
                {
                    var client = new WebClient();
					data = client.DownloadData(uri);
				} 
				catch (WebException) 
				{
                    // Do nothing, call callback with null as argument
				}
				callback(data);
            });
		}
	}
}

