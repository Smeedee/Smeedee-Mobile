using System;
using MonoTouch.UIKit;

namespace Smeedee.iOS.Lib
{
	public static class StyleExtensions
	{
		public static readonly UIColor transparent = UIColor.FromWhiteAlpha(1.0f, 0.0f);
		
        public static readonly UIColor smeedeeOrange = UIColor.FromRGB(242, 80, 0); //f25000
		public static readonly UIColor descriptionColor = UIColor.FromRGB(255, 255, 255); //1d1d1d
		
		public static readonly UIColor blackBackground = UIColor.FromRGB(0, 0, 0);
		
		public static readonly UIColor grayTableCell = UIColor.FromRGB(70, 70, 70);//UIColor.FromWhiteAlpha(0.7f, 0.7f);
		public static readonly UIColor lightGrayText = UIColor.FromRGB(218, 218, 218);
		
		public static readonly UIColor darkGrayText = UIColor.FromRGB(150, 150, 150);
		public static readonly UIColor darkGrayTextHighlight = UIColor.FromRGB(50, 50, 50);
		
		public static readonly UIColor darkGrayHeadline = UIColor.FromWhiteAlpha(0.5f, 0.5f);
		
		public static readonly UIColor tableSeparator = UIColor.FromRGB(20, 20, 20);
		
		public static void StyleAsWidgetHeadline(this UILabel self)
		{
			self.TextColor = smeedeeOrange;
		}
		
		public static void StyleAsHeadline(this UILabel self) 
		{
			self.TextColor = smeedeeOrange;
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
			self.IndicatorStyle = UIScrollViewIndicatorStyle.White;
		}
		
		public static void StyleAsSettingsTableCell(this UITableViewCell self)
		{
			self.BackgroundColor = grayTableCell;
			self.TextLabel.TextColor = lightGrayText;
			self.TextLabel.BackgroundColor = transparent;
			self.TextLabel.HighlightedTextColor = UIColor.Black;
			if (self.DetailTextLabel != null) {
				self.DetailTextLabel.TextColor = darkGrayText;
				self.DetailTextLabel.BackgroundColor = transparent;
				self.DetailTextLabel.HighlightedTextColor = darkGrayTextHighlight;
			}
			self.SelectionStyle = UITableViewCellSelectionStyle.Gray;
		}
	}
}

