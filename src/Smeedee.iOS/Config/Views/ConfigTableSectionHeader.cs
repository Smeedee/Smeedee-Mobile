using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using Smeedee.iOS;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
	public class ConfigTableSectionHeader : UIView
	{
		private static int padding = Platform.Name == Device.Iphone ? 15 : 45;
		public static int Height = 35;
		
		private UILabel label;
		
		public ConfigTableSectionHeader(string header) : base() {
			this.Frame = new RectangleF(padding, 0, 320 - 2*padding, Height);
			
			label = new UILabel(this.Frame);
			this.AddSubview(label);
			
			StyleContainer();
			StyleLabel(header);
		}
		
		private void StyleContainer() {
			this.BackgroundColor = StyleExtensions.transparent;
		}
		
		private void StyleLabel(string header) {
			label.Text = header;
			label.BackgroundColor = StyleExtensions.transparent;
			label.Font = UIFont.BoldSystemFontOfSize(17);
			label.TextColor = StyleExtensions.smeedeeOrange;
		}
	}
}

