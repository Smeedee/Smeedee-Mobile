using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class BuildStatusConfigTableViewController : UITableViewController
	{
		public BuildStatusConfigTableViewController() : base ("BuildStatusConfigTableViewController", null)
		{
			this.Title = "Build status";
			this.TableView.Source = new BuildStatusConfigTableSource(this);
			
			TableView.StyleAsSettingsTable();
		}
	}
	
	public class BuildStatusConfigTableSource : WidgetConfigTableViewSource
	{
		private UITableViewController controller;
		private BuildStatus model;
		
		private UISwitch topSwitch;
		//private UISwitch nameSwitch;
		
		public BuildStatusConfigTableSource(UITableViewController controller) 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(BuildStatusConfigTableViewController)).First())
		{
			this.controller = controller;
			this.model = new BuildStatus();
		}
		
		public override int NumberOfSections (UITableView tableView) 
		{ 
			return 2; 
		}
		
		public override int RowsInSection(UITableView tableView, int section)
		{
			if (section == 0) 
				return base.RowsInSection(tableView, section);
			return 2;
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			int section = indexPath.Section;
			int row = indexPath.Row;
			
			if (section == 0) 
				return base.GetCell(tableView, indexPath);
			
			if (row == 0)
			{
				topSwitch = new UISwitch();
				topSwitch.SetState(model.BrokenBuildsAtTop, false);
				topSwitch.ValueChanged += delegate {
					model.BrokenBuildsAtTop = topSwitch.On;
				};
				
				var cell = new UITableViewCell(UITableViewCellStyle.Default, "SimpleCheckboxCell") {
					AccessoryView = topSwitch, 
					SelectionStyle = UITableViewCellSelectionStyle.None
				};
				cell.TextLabel.Text = "Broken builds first";
				
				cell.StyleAsSettingsTableCell();
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				return cell;
			}
			else
			{
				var cell = new UITableViewCell(UITableViewCellStyle.Default, "SubtitleDisclosureCell") {
					Accessory = UITableViewCellAccessory.DisclosureIndicator
				};
				cell.TextLabel.Text = "Build order";
				
				// TODO: Gray text to the right of what is currently selected
				cell.StyleAsSettingsTableCell();
				return cell;
			}
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var section = indexPath.Section;
			var row = indexPath.Row;
			
			if (section == 1)
			{
				if (row == 1) {
					var values = new List<BuildOrder>() { BuildOrder.BuildName, BuildOrder.BuildTime };
					
					var groupController = 
						new RadioGroupTableViewController("Build ordering", new [] {"Build name", "Build time"}, values.IndexOf(model.Ordering));
					
					groupController.RowSelected = delegate(int n) {
						model.Ordering = values[n];
						Console.WriteLine("Changed ordering to " + values[n]);
					};
					
					controller.NavigationController.PushViewController(groupController, true);
				}
			}
		}
		public override UIView GetViewForHeader (UITableView tableView, int section)
		{	
			switch (section) {
			case 0:
				return base.GetViewForHeader(tableView, section);
			case 1:
				return new ConfigTableSectionHeader("Ordering");
			default:
				return new ConfigTableSectionHeader("Display");
			}
		}

		
	}
}

