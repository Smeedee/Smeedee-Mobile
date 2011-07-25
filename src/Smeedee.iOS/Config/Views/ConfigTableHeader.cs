using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace Smeedee.iOS
{
	public class ConfigTableHeader : UILabel
	{
		public ConfigTableHeader(string header) : base() {
			this.Text = header;
			this.TextColor = UIColor.Red;
		}
	}
}

