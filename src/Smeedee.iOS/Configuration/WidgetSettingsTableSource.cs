using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS.Configuration
{
	public class WidgetSettingsTableSource<T> : UITableViewSource where T : IWidget
	{
		
		// Needs to be declared here to avoid GC issues
		// See http://stackoverflow.com/questions/6156165/why-does-my-uiswitch-crash-when-it-is-a-tableview-cell-accessoryview
		//
		private UISwitch enabledSwitch;
		protected IPersistenceService persister = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "ENABLE_WIDGET");
			cell.TextLabel.Text = "Enabled";
			var className = typeof(T).Name;
			
			var enabled = persister.Get<Dictionary<string, bool>>("EnabledWidgets", null);
			
			enabledSwitch = new UISwitch();
			if (!enabled.ContainsKey(className)) {
				enabled[className] = true;
				persister.Save("EnabledWidgets", enabled);
			}
			
			enabledSwitch.SetState(enabled[className], false);
			
			enabledSwitch.ValueChanged += delegate {
				var enabledWidgets = persister.Get<Dictionary<string, bool>>("EnabledWidgets", null);
				enabledWidgets[className] = enabledSwitch.On;
				persister.Save("EnabledWidgets", enabledWidgets);
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

