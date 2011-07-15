using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS.Configuration
{
	public class TopComittersTableSource : UITableViewSource
	{
		private UISwitch enabledSwitch;
		
		public TopComittersTableSource() : base() {
		}
		
		public override int RowsInSection (UITableView tableview, int section)
		{
			return 1;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0) {
				var cell = new UITableViewCell(UITableViewCellStyle.Default, "ENABLE_WIDGET");
				cell.TextLabel.Text = "Enabled";
				
				enabledSwitch = new UISwitch();
				enabledSwitch.ValueChanged += delegate {
					Console.WriteLine("View switch value is changed");
				};
				
				cell.AccessoryView = enabledSwitch;
				return cell;
			}
			return null;
		}
	}
}

