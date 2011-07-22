using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public partial class RadioGroupTableViewController : UITableViewController
	{
		public int Selected { get; set; }
		
		public Action<int> RowSelected { get; set; }
		
		public RadioGroupTableViewController(string headline, IList<string> labels, int selected) : base ("RadioGroupTableViewController", null)
		{
			RowSelected = (n) => { };
			
			TableView.Source = new RadioGroupTableViewSource(this, headline, labels, selected);
		}
	}
	
	public class RadioGroupTableViewSource : UITableViewSource
	{
		private RadioGroupTableViewController controller;
		
		private UITableViewCell[] cell;
		private int selected;
		
		private string headline;
		private IList<string> labels;
		
		public RadioGroupTableViewSource(RadioGroupTableViewController controller, string headline, IList<string> labels, int selected) 
		{
			this.cell = new UITableViewCell[labels.Count];
			this.controller = controller;
			this.headline = headline;
			this.selected = selected;
			this.labels = labels;
		}
		
		public override int NumberOfSections (UITableView tableView) { return 1; }
		public override int RowsInSection(UITableView tableView, int section) {	return labels.Count; }
		public override string TitleForHeader (UITableView tableView, int section) { return headline; }
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var row = indexPath.Row;
			
			cell[row] = new UITableViewCell(UITableViewCellStyle.Default, "DefaultCheckmarkCell") {
				Accessory = UITableViewCellAccessory.None
			};
			
			if (row == selected)
				cell[row].Accessory = UITableViewCellAccessory.Checkmark;
			
			cell[row].TextLabel.Text = labels[row];
			
			return cell[row];
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var row = indexPath.Row;
			
			cell[selected].Accessory = UITableViewCellAccessory.None;
			cell[row].Accessory = UITableViewCellAccessory.Checkmark;
			selected = row;
			
			Console.WriteLine("Row selected: " + row);
			controller.RowSelected(row);
			
			cell[row].SetSelected(false, true);
		}
	}
}

