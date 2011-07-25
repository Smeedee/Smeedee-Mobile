using System;
using MonoTouch.UIKit;
namespace Smeedee.iOS
{
	public static class StyleExtensions
	{
		public static readonly UIColor transparent = UIColor.FromWhiteAlpha(1.0f, 0.0f);
		
        public static readonly UIColor headlineColor = UIColor.FromRGB(242, 80, 0); //f25000
		public static readonly UIColor descriptionColor = UIColor.FromRGB(255, 255, 255); //1d1d1d
		
		public static readonly UIColor blackBackground = UIColor.FromRGB(0, 0, 0);
		
		public static readonly UIColor grayTableCell = UIColor.FromRGB(70, 70, 70);//UIColor.FromWhiteAlpha(0.7f, 0.7f);
		public static readonly UIColor lightGrayText = UIColor.FromRGB(218, 218, 218);
		public static readonly UIColor darkGrayText = UIColor.FromRGB(121, 121, 121);
		
		public static readonly UIColor darkGrayHeadline = UIColor.FromWhiteAlpha(0.4f, 0.5f);
		
		public static readonly UIColor tableSeparator = UIColor.FromWhiteAlpha(0.5f, 0.8f);
		
		
		
		public static void StyleAsHeadline(this UILabel self) 
		{
			self.TextColor = headlineColor;				
		}
		
		public static void StyleAsDescription(this UILabel self) 
		{
			self.TextColor = descriptionColor;
		}
		
		public static void StyleAsSettingsTable(this UITableView self)
		{
			self.BackgroundColor = blackBackground;
			self.SeparatorColor = tableSeparator;
			self.SeparatorStyle = UITableViewCellSeparatorStyle.SingleLine;
			//self.IndicatorStyle = UIScrollViewIndicatorStyle.Black;
		}
		
		public static void StyleAsSettingsTableCell(this UITableViewCell self)
		{
			self.BackgroundColor = grayTableCell;
			self.TextLabel.TextColor = lightGrayText;
			self.TextLabel.BackgroundColor = transparent;
			if (self.DetailTextLabel != null) {
				self.DetailTextLabel.TextColor = darkGrayText;
				self.DetailTextLabel.BackgroundColor = transparent;
			}
			self.SelectionStyle = UITableViewCellSelectionStyle.Gray;
		}
	}
}

