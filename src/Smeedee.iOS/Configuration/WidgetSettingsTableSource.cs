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
			
			// Very ugly, needs refactoring
			var className = typeof(T).Name;
			var isEnabled = persister.Get(className, true);
			
			enabledSwitch = new UISwitch();
			enabledSwitch.SetState(isEnabled, false);
			
			enabledSwitch.ValueChanged += delegate {
				persister.Save(className, enabledSwitch.On);
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

