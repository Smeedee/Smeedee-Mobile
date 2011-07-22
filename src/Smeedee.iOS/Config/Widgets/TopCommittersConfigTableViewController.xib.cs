using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class TopCommittersConfigTableViewController : UITableViewController
	{
		public TopCommittersConfigTableViewController() : base("TopCommittersConfigTableViewController", null)
		{
			this.Title = "Top Committers";
			this.TableView.Source = new TopCommittersConfigTableSource(this);
		}
	}
	
	public class TopCommittersConfigTableSource : WidgetConfigTableViewSource
	{
		private TopCommitters model;
		private UITableViewController controller;
		
		// Here is actually where we decide possible values. 
		// Can differentiate from Android. Only common is that default 5 is stored in model.
		private IList<int> committerCountValues = new[] { 5, 10, 15 };
		private int committerCountSelected;
		private IList<UITableViewCell> committerCountCells = new List<UITableViewCell>();
		
		public TopCommittersConfigTableSource(UITableViewController controller) 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(TopCommittersConfigTableViewController)).First())
		{
			this.controller = controller;
			model = new TopCommitters();
			Console.WriteLine("Created source");
			committerCountSelected = committerCountValues.IndexOf(model.NumberOfCommitters);
		}
		
		public override int NumberOfSections (UITableView tableView)
		{
			return 2;
		}
		
		public override int RowsInSection (UITableView tableview, int section)
		{
			return (section == 0) ? 1 : 3;
		}
		
		public override string TitleForHeader (UITableView tableView, int section)
		{
			return (section == 0) ? base.TitleForHeader(tableView, section) : "Number of committers";
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0) return base.GetCell(tableView, indexPath);
			
			Console.WriteLine("Getting row at index " + indexPath.Row);
			
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "RadioButtonTableCell") {
				Accessory = UITableViewCellAccessory.None
			};
			
			if (indexPath.Row == committerCountSelected) 
				cell.Accessory = UITableViewCellAccessory.Checkmark;
			
			cell.TextLabel.Text = string.Format("Top {0} committers", committerCountValues[indexPath.Row]);
			committerCountCells.Add(cell);
			return cell;
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			committerCountCells[committerCountSelected].Accessory = UITableViewCellAccessory.None;
			
			committerCountSelected = indexPath.Row;
			
			committerCountCells[indexPath.Row].Accessory = UITableViewCellAccessory.Checkmark;
			
			model.NumberOfCommitters = committerCountValues[indexPath.Row];
			Console.WriteLine("Selecting row " + indexPath.Row);
			
		}
	}
}
