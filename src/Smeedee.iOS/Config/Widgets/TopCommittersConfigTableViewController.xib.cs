using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
	public partial class TopCommittersConfigTableViewController : UITableViewController
	{
		public TopCommittersConfigTableViewController() : base("TopCommittersConfigTableViewController", null)
		{
			this.Title = "Top Committers";
			this.TableView.Source = new TopCommittersConfigTableSource(this);
			
			TableView.StyleAsSettingsTable();
		}
	}
	
	public class TopCommittersConfigTableSource : WidgetConfigTableViewSource
	{
		private TopCommitters model;
		private UITableViewController controller;
		
		private IList<UITableViewCell> countCells = new List<UITableViewCell>(3);
		private IList<int> countValues = new[] { 5, 10, 15 };
		private int countSelected;
		
		private IList<UITableViewCell> timeCells = new List<UITableViewCell>(3);
		private IList<TimePeriod> timeValues = new[] { TimePeriod.PastDay, TimePeriod.PastWeek, TimePeriod.PastMonth };
		private int timeSelected;
		
		public TopCommittersConfigTableSource(UITableViewController controller) 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(TopCommittersConfigTableViewController)).First())
		{
			this.controller = controller;
			model = new TopCommitters();
			countSelected = countValues.IndexOf(model.NumberOfCommitters);
			timeSelected = timeValues.IndexOf(model.TimePeriod);
		}
		
		private bool IsNumberOfCommitters(int section) { return section == 1; }
		private bool IsTimePeriod(int section) { return section == 2; }
		
		public override int NumberOfSections (UITableView tableView) { return 3; }
		
		public override int RowsInSection(UITableView tableView, int section)
		{
			if (IsNumberOfCommitters(section)) 
				return countValues.Count;
			if (IsTimePeriod(section)) 
				return timeValues.Count;
			return base.RowsInSection(tableView, section);
		}
		
		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0) 
				return base.GetCell(tableView, indexPath);
			
			var cell = new UITableViewCell(UITableViewCellStyle.Default, string.Format("RadioButtonTableCell{0}{1}", indexPath.Section, indexPath.Row)) {
				AccessoryView = null
			};
			
			if (IsNumberOfCommitters(indexPath.Section)) 
			{
				if (indexPath.Row == countSelected) 
					cell.AccessoryView = RadioGroupTableViewSource.BlackAccessoryCheckmark();
				cell.TextLabel.Text = string.Format("Top {0} committers", countValues[indexPath.Row]);
				countCells.Insert(indexPath.Row, cell);
			}
			
			if (IsTimePeriod(indexPath.Section))
			{
				if (indexPath.Row == timeSelected) 
					cell.AccessoryView = RadioGroupTableViewSource.BlackAccessoryCheckmark();
				cell.TextLabel.Text = string.Format("Past {0}", timeValues[indexPath.Row].ToSuffix());
				timeCells.Insert(indexPath.Row, cell);
			}
			
			cell.StyleAsSettingsTableCell();
			
			return cell;
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			if (IsNumberOfCommitters(indexPath.Section)) 
			{
				// Ugly fix for nullpointer, happens when you click a cell that is not yet created by GetCell
				if (countCells[countSelected] == null) {
					return;
				}
				countCells[countSelected].AccessoryView = null;
				countCells[indexPath.Row].AccessoryView = RadioGroupTableViewSource.BlackAccessoryCheckmark();
				countSelected = indexPath.Row;
				
				model.NumberOfCommitters = countValues[countSelected];
				
				countCells[countSelected].SetSelected(false, true);
			}
			
			if (IsTimePeriod(indexPath.Section)) 
			{
				// Ugly fix for nullpointer, happens when you click a cell that is not yet created by GetCell
				if (timeCells[timeSelected] == null) {
					return;
				}
				timeCells[timeSelected].AccessoryView = null;
				timeCells[indexPath.Row].AccessoryView = RadioGroupTableViewSource.BlackAccessoryCheckmark();
				timeSelected = indexPath.Row;
				
				model.TimePeriod = timeValues[timeSelected];
				
				timeCells[timeSelected].SetSelected(false, true);
			}
		}
	}
}
