using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS.Configuration
{
	public class BuildStatusTableSource : UITableViewSource
	{
		// Needs to be declared here to avoid GC issues
		// See http://stackoverflow.com/questions/6156165/why-does-my-uiswitch-crash-when-it-is-a-tableview-cell-accessoryview
		//
		private UISwitch enabledSwitch;
		private IPersistenceService persister = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
		
		public BuildStatusTableSource () : base() { }
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0)
			{
				var cell = new UITableViewCell(UITableViewCellStyle.Default, "ENABLE_WIDGET");
				cell.TextLabel.Text = "Enabled";
				var className = typeof(BuildStatusWidget).Name;
				
				var enabled = persister.Get<Dictionary<string, bool>>("EnabledWidgets", null);
				
				enabledSwitch = new UISwitch();
				var isEnabled = true;
				enabled.TryGetValue(className, out isEnabled);
				enabledSwitch.SetState(isEnabled, false);
				
				enabledSwitch.ValueChanged += delegate {
					Console.WriteLine(string.Format("View switch value is {0}", enabledSwitch.On));
					
					var enabledWidgets = persister.Get<Dictionary<string, bool>>("EnabledWidgets", null);
					enabledWidgets[className] = enabledSwitch.On;
					persister.Save("EnabledWidgets", enabledWidgets);
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

