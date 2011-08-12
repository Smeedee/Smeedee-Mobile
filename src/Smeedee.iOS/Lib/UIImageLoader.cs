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
		private static ILog logger = SmeedeeApp.Instance.ServiceLocator.Get<ILog>();
		private static UIImage defaultImage = UIImage.FromFile("Images/default_person.jpeg");
		
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
				if (result != null)
				{
					try 
					{
						return UIImage.LoadFromData(NSData.FromArray(result));
					}
					catch (Exception e)
					{
						logger.Log("Error converting to UIImage", e.Message);
					}
				}
				return defaultImage;
			}
		}
	}
}
