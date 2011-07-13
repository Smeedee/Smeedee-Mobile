using System;
using MonoTouch.UIKit;
namespace Smeedee.iOS
{
	public static class StyleExtensions
	{
        public static readonly UIColor headlineColor = UIColor.FromRGB(242, 80, 0); //f25000
		public static readonly UIColor descriptionColor = UIColor.FromRGB(255, 255, 255); //1d1d1d
		
		public static void StyleAsHeadline(this UILabel self) 
		{
			self.TextColor = headlineColor;				
		}
		
		public static void StyleAsDescription(this UILabel self) 
		{
			self.TextColor = descriptionColor;
		}
	}
}

