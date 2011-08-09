using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
	public partial class LatestCommitsLoadMoreTableCellController : TableViewCellController
	{
		public static readonly int Height = 40;
		
		public override UITableViewCell TableViewCell {
			get {
				return cell;
			}
		}
		
		public LatestCommitsLoadMoreTableCellController() : base ("LatestCommitsLoadMoreTableCellController", null)
		{
		}
		
		public void ApplyStyling()
		{
			var frame = new RectangleF(cell.Frame.X - 2, cell.Frame.Y, cell.Frame.Width, Height);
			cell.Frame = frame;
			label.Frame = frame;
			
			cell.BackgroundColor = StyleExtensions.blackBackground;
			
			label.BackgroundColor = StyleExtensions.tableSeparator;
			label.TextColor = StyleExtensions.lightGrayText;
			
			label.HighlightedTextColor = StyleExtensions.blackBackground;
		}
	}
}

