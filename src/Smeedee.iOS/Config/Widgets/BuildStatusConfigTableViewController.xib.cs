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
		}
	}
	
	public class BuildStatusConfigTableSource : WidgetConfigTableViewSource
	{
		private UITableViewController controller;
		private BuildStatus model;
		
		private UISwitch topSwitch;
		private UISwitch nameSwitch;
		
		public BuildStatusConfigTableSource(UITableViewController controller) 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(BuildStatusConfigTableViewController)).First())
		{
			this.controller = controller;
			this.model = new BuildStatus();
		}
		
		public override int NumberOfSections (UITableView tableView) 
		{ 
			return base.NumberOfSections(tableView) + 2; 
		}
		
		public override int RowsInSection(UITableView tableView, int section)
		{
			if (section == 0) 
				return base.RowsInSection(tableView, section);
			if (section == 1) 
				return 2;
			return 1;
		}
		
		public override string TitleForHeader(UITableView tableView, int section)
		{
			if (section == 1) 
				return "Ordering";
			if (section == 2) 
				return "Display";
			return base.TitleForHeader(tableView, section);
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			int section = indexPath.Section;
			int row = indexPath.Row;
			
			if (section == 0) 
				return base.GetCell(tableView, indexPath);
			
			if (section == 1)
			{
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
					
					return cell;
				}
				else
				{
					var cell = new UITableViewCell(UITableViewCellStyle.Default, "SubtitleDisclosureCell") {
						Accessory = UITableViewCellAccessory.DisclosureIndicator
					};
					cell.TextLabel.Text = "Build order";
					// TODO: Gray text to the right of what is currently selected
					return cell;
				}
			}
			else
			{
				nameSwitch = new UISwitch();
				nameSwitch.SetState(model.BrokenBuildsAtTop, false);
				nameSwitch.ValueChanged += delegate {
					model.ShowTriggeredBy = nameSwitch.On;
				};
				
				var cell = new UITableViewCell(UITableViewCellStyle.Default, "SimpleCheckboxCell") {
					AccessoryView = nameSwitch, 
					SelectionStyle = UITableViewCellSelectionStyle.None
				};
				cell.TextLabel.Text = "Show username";
				
				return cell;
			}
			
			
			return new UITableViewCell(UITableViewCellStyle.Default, "WAT");
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var section = indexPath.Section;
			var row = indexPath.Row;
			
			if (section == 1)
			{
				if (row == 1) {
					BuildOrder[] values = { BuildOrder.BuildName, BuildOrder.BuildTime };
					
					var groupController = 
						new RadioGroupTableViewController("Build ordering", new [] {"Build name", "Build time"});
					
					groupController.RowSelected = delegate(int n) {
						model.Ordering = values[n];
						Console.WriteLine("Changed ordering to " + values[n]);
					};
					
					controller.NavigationController.PushViewController(groupController, true);
				}
			}
		}
	}
}

