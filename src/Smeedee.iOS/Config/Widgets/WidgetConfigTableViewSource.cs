using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public class WidgetConfigTableViewSource : UITableViewSource
	{
		// Needs to be declared here to avoid GC issues
		// See http://stackoverflow.com/questions/6156165/why-does-my-uiswitch-crash-when-it-is-a-tableview-cell-accessoryview
		private UISwitch enabledSwitch;
		
		protected WidgetModel widgetModel;
		
		public WidgetConfigTableViewSource(WidgetModel model)
		{
			widgetModel = model;
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			enabledSwitch = new UISwitch();
			enabledSwitch.SetState(widgetModel.Enabled, false);
			enabledSwitch.ValueChanged += delegate {
				widgetModel.Enabled = enabledSwitch.On;
			};
			
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "EnableWidgetUISwitch");
			cell.AccessoryView = enabledSwitch;
			cell.TextLabel.Text = "Enabled";
			return cell;
		}
		
        public override int NumberOfSections(UITableView tableView) { return 1; }
		
        public override int RowsInSection(UITableView tableview, int section) { return 1; }
        
        public override string TitleForHeader(UITableView tableView, int section) { return "General"; }
	}
}

