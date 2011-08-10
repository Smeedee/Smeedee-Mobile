using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;
using Smeedee.iOS.Lib;

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
		
		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			Platform.Orientation = toInterfaceOrientation;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
	
	public class MainConfigTableSource : UITableViewSource
    {
		private MainConfigTableViewController controller;
		
		public MainConfigTableSource(MainConfigTableViewController controller) : base() {
			this.controller = controller;
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
				/* Calendar icon test
				if (indexPath.Row == 2)
				{
					cell.TextLabel.Frame = new RectangleF(cell.TextLabel.Frame.X + 60, cell.TextLabel.Frame.Y, cell.TextLabel.Frame.Width - 60, cell.TextLabel.Frame.Height);
					cell.DetailTextLabel.Frame = new RectangleF(cell.DetailTextLabel.Frame.X + 60, cell.DetailTextLabel.Frame.Y, cell.DetailTextLabel.Frame.Width - 60, cell.DetailTextLabel.Frame.Height);
					
					var image = new UIImageView(UIImage.FromFile("images/calendar.png")) { 
						Frame = new RectangleF(7, 4, 40, 40)
					};
					
					cell.AddSubview(image);
				}*/
					
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
		
		public override UIView GetViewForHeader (UITableView tableView, int section)
		{
			if (section == 1)
				return new ConfigTableSectionHeader("Widgets");
			return null;
		}
		
		public override float GetHeightForHeader (UITableView tableView, int section)
		{
			if (section == 1)
				return ConfigTableSectionHeader.Height;
			return 0;
		}
	}
}

