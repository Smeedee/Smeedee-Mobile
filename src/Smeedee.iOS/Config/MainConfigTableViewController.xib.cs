using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class MainConfigTableViewController : UITableViewController
	{
		public MainConfigTableViewController() : base("MainConfigTableViewController", null) 
		{ 
			this.Title = "Settings";
			this.TableView.Source = new MainConfigTableSource(this);
			
			TableView.StyleAsSettingsTable();
		}
	}
	
	public class MainConfigTableSource : UITableViewSource
    {
		private MainConfigTableViewController controller;
		private Login loginModel;
		
		public MainConfigTableSource(MainConfigTableViewController controller) : base() {
			this.controller = controller;
			loginModel = new Login();
		}
		
        public override int NumberOfSections(UITableView tableView) { return 2; }
        public override int RowsInSection(UITableView tableview, int section)
        {
			return (section == 0) ? 1 : SmeedeeApp.Instance.AvailableWidgets.Count;
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			UITableViewCell cell;
			switch (indexPath.Section) 
			{
			case 0:
	            cell = tableView.DequeueReusableCell("CellID") ??
	                       new UITableViewCell(UITableViewCellStyle.Subtitle, "CellID");
	            
	            cell.TextLabel.Text = "Smeedee server";
	            cell.DetailTextLabel.Text = "Configure login url and password";
            	cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
	            cell.StyleAsSettingsTableCell();
				
	            return cell;
				
			default:
				var widget = SmeedeeApp.Instance.AvailableWidgets.ElementAt(indexPath.Row);
				
	            cell = tableView.DequeueReusableCell("CellID") ??
	                       new UITableViewCell(UITableViewCellStyle.Subtitle, "CellID");
	            
	            cell.TextLabel.Text = widget.Name;
				cell.TextLabel.HighlightedTextColor = UIColor.Black;
	            cell.DetailTextLabel.Text = widget.StaticDescription;
            	cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
	            cell.StyleAsSettingsTableCell();
				
	            return cell;
			}
        }
		
        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
			if (indexPath.Section == 0) {
				var instance = new ServerConfigTableViewController();
				controller.NavigationController.PushViewController(instance, true);
			}
			else
			{
				var widgetModel = SmeedeeApp.Instance.AvailableWidgets.ElementAt(indexPath.Row);
				if (widgetModel.SettingsType != null) 
				{
					var settingsControllerInstance = Activator.CreateInstance(widgetModel.SettingsType) as UIViewController;
					controller.NavigationController.PushViewController(settingsControllerInstance, true);
				}
			}
        }
	}
}

