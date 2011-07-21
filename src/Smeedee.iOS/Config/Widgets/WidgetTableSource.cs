using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public class WidgetTableSource : UITableViewSource
	{
		// Needs to be declared here to avoid GC issues
		// See http://stackoverflow.com/questions/6156165/why-does-my-uiswitch-crash-when-it-is-a-tableview-cell-accessoryview
		//
		private UISwitch enabledSwitch;
		
		// Changes to the enabled state goes through this model
		protected WidgetModel widgetModel;
		
		public WidgetTableSource(WidgetModel model)
		{
			widgetModel = model;
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "ENABLE_WIDGET");
			cell.TextLabel.Text = "Enabled";
			
			// Not nice at all :(
			//var widgetAttributes = typeof(T).GetCustomAttributes(typeof(WidgetAttribute), true);
			//var enabledKey = (widgetAttributes.First() as WidgetAttribute).Name;
			//var isEnabled = persister.Get(enabledKey, false);
			
			enabledSwitch = new UISwitch();
			enabledSwitch.SetState(true, false); // widgetModel.Enabled
			
			enabledSwitch.ValueChanged += delegate {
				// Want to do this: 
				//widgetModel.Enabled = enabledSwitch.On;
				
				//persister.Save(enabledKey, enabledSwitch.On);
			};
			
			cell.AccessoryView = enabledSwitch;
			return cell;
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

