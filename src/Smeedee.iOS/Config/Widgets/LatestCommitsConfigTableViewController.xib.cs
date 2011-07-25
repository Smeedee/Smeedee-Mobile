using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class LatestCommitsConfigTableViewController : UITableViewController
	{
		public LatestCommitsConfigTableViewController() : base ("LatestCommitsConfigTableViewController", null)
		{
			Title = "Latest commits";
			TableView.Source = new LatestCommitsConfigTableViewSource(this);
			
			TableView.StyleAsSettingsTable();
		}
	}
	
	public class LatestCommitsConfigTableViewSource : WidgetConfigTableViewSource
	{
		private UITableViewController controller;
		private LatestCommits model;
		
		private UISwitch emptySwitch;
		
		public LatestCommitsConfigTableViewSource(UITableViewController controller) 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(LatestCommitsConfigTableViewController)).First())
		{
			this.controller = controller;
			this.model = new LatestCommits();
		}
		
		public override int NumberOfSections (UITableView tableView) { return 2; }
		public override int RowsInSection (UITableView tableView, int section)
		{
			if (section == 0) 
				return base.RowsInSection(tableView, section);
			return 2;
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var section = indexPath.Section;
			var row = indexPath.Row;
			
			if (section == 0) 
				return base.GetCell(tableView, indexPath);
			
			if (row == 0)
			{
				emptySwitch = new UISwitch();
				emptySwitch.SetState(true, false); // TODO
				emptySwitch.ValueChanged += delegate {
					//model.WAT = emptySwitch.On;
				};
				
				var cell = new UITableViewCell(UITableViewCellStyle.Default, "SimpleCheckboxCell") {
					AccessoryView = emptySwitch, 
					SelectionStyle = UITableViewCellSelectionStyle.None
				};
				
				cell.TextLabel.Text = "Highlight empty commits";
				cell.StyleAsSettingsTableCell();
				cell.SelectionStyle = UITableViewCellSelectionStyle.None;
				return cell;
			}
			else
			{
				var cell = new UITableViewCell(UITableViewCellStyle.Default, "SubtitleDisclosureCell") {
					Accessory = UITableViewCellAccessory.DisclosureIndicator
				};
				cell.TextLabel.Text = "Highlight color";
				// TODO: Gray text to the right of what is currently selected
				cell.StyleAsSettingsTableCell();
				return cell;
			}
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var section = indexPath.Section;
			var row = indexPath.Row;
			
			if (row == 1)
			{
				var groupController = new RadioGroupTableViewController("Color", new [] {"Red", "Green", "Blue"}, 1);
				controller.NavigationController.PushViewController(groupController, true);
				// ConfigTableHeader here?
			}
		}
		public override UIView GetViewForHeader (UITableView tableView, int section)
		{	
			switch (section) {
			case 0:
				return base.GetViewForHeader(tableView, section);
			default:
				return new ConfigTableHeader("Highlight color");
			}
		}
	}
}

