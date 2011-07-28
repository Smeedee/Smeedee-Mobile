using System;
using Smeedee.Model;
using Smeedee.Services;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS.Lib
{
	public class UIImageLoader
	{
		private IImageService service;
		
		public UIImageLoader()
		{
			service = SmeedeeApp.Instance.ServiceLocator.Get<IImageService>();
		}
		
		public void LoadImageFromUri(Uri uri, Action<UIImage> callback) 
		{
			service.GetImage(uri, (bytes) => {
				UIImage img = UIImage.LoadFromData(NSData.FromArray(bytes));
				callback(img);
			});
		}
	}
}

