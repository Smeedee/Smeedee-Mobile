using System;
using System.Collections.Generic;
using Smeedee.Model;
using Smeedee.Services;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS.Lib
{
	public class UIImageLoader
	{
		private static Dictionary<Uri, UIImage> cache = new Dictionary<Uri, UIImage>();
		
		private IImageService service;
		
		public UIImageLoader()
		{
			service = SmeedeeApp.Instance.ServiceLocator.Get<IImageService>();
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
					cache[uri] = UIImage.LoadFromData(NSData.FromArray(bytes));
					callback(cache[uri]);
				});
			}
		}
	}
}

