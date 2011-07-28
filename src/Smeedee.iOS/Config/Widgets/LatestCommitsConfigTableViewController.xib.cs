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
		public override int RowsInSection (UITableView tableView, int section) { return 1; }
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0) 
				return base.GetCell(tableView, indexPath);
			
			emptySwitch = new UISwitch();
			emptySwitch.SetState(model.HighlightEmpty, false);
			emptySwitch.ValueChanged += delegate {
				Console.WriteLine("Setting state to " + emptySwitch.On);
				model.HighlightEmpty = emptySwitch.On;
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
	}
}

