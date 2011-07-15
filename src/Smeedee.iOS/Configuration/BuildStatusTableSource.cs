using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS.Configuration
{
	public class BuildStatusTableSource : WidgetSettingsTableSource<BuildStatusWidget>
	{
		
		public BuildStatusTableSource () : base() { }
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0)
			{
				return base.GetCell(tableView, indexPath);
			}
			else
			{
				var cell = new UITableViewCell();
				cell.TextLabel.Text = "Option";
				return cell;
			}
		}
	}
}

