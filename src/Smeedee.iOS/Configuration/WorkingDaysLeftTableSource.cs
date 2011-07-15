using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS.Configuration
{
	public class WorkingDaysLeftTableSource : UITableViewSource
	{
		// Needs to be declared here to avoid GC issues
		// See http://stackoverflow.com/questions/6156165/why-does-my-uiswitch-crash-when-it-is-a-tableview-cell-accessoryview
		//
		private UISwitch enabledSwitch;
		
		public WorkingDaysLeftTableSource () : base() { }
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0)
			{
				var cell = new UITableViewCell(UITableViewCellStyle.Default, "ENABLE_WIDGET");
				cell.TextLabel.Text = "Enabled";
				
				enabledSwitch = new UISwitch();
				enabledSwitch.ValueChanged += delegate {
					Console.WriteLine(string.Format("View switch value is {0}", enabledSwitch.On));
				};
				
				cell.AccessoryView = enabledSwitch;
				return cell;
			}
			else
			{
				var cell = new UITableViewCell();
				cell.TextLabel.Text = "Option";
				return cell;
			}
		}
		
        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }
		
        public override int RowsInSection(UITableView tableview, int section)
        {
			return 1;
        }
        
        public override string TitleForHeader(UITableView tableView, int section)
        {
			return "General";
        }
	}
}

