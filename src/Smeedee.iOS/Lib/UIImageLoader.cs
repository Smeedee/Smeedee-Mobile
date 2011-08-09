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
		
		public static void LoadImageFromUri(Uri uri, Action<UIImage> callback) 
		{
			var service = SmeedeeApp.Instance.ServiceLocator.Get<IImageService>();
			
			service.GetImage(uri, (bytes) => {
				var image = ImageFromBytes(bytes);
				callback(image);
			});
		}
		
		private static UIImage ImageFromBytes(byte[] result)
		{	
			using (var pool = new NSAutoreleasePool())
			{
				// InvalidCastException in NSData.FromBytes
				return (result != null) ? UIImage.LoadFromData(NSData.FromArray(result)) : defaultImage;
			}
		}
	}
}

