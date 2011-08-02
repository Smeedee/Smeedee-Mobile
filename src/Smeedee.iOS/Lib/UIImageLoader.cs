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
		private static Dictionary<Uri, UIImage> cache = new Dictionary<Uri, UIImage>();
		
		private IImageService service;
		
		public UIImageLoader()
		{
			service = new TrivialImageLoader();//SmeedeeApp.Instance.ServiceLocator.Get<IImageService>();
		}
		
		public void LoadImageFromUri(Uri uri, Action<UIImage> callback) 
		{
			if (cache.ContainsKey(uri)) 
			{
				callback(cache[uri]);
			}
			else
			{
				service.GetImage(uri, (bytes) => {
					using (var pool = new NSAutoreleasePool())
					{
						cache[uri] = (bytes != null) ? UIImage.LoadFromData(NSData.FromArray(bytes)) : defaultImage;
						callback(cache[uri]);
					}
				});
			}
		}
	}
	
	internal class TrivialImageLoader : IImageService
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
				catch (WebException e) 
				{
					Console.WriteLine("Image error: " + e.Message);
                    //Do nothing, call callback with null as argument
				}
				
				Console.WriteLine("Returning image data: " + data);
				
				callback(data);
            });
		}
	}
}

