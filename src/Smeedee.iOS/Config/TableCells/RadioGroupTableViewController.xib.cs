using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public partial class RadioGroupTableViewController : UITableViewController
	{
		public IList<int> Values { get; private set; }
		public string Headline { get; private set; }
		public int Selected { get; set; }
		
		public Action<int> RowSelected { get; set; }
		
		public RadioGroupTableViewController(string headline, IList<int> values, int selected) : base ("RadioGroupTableViewController", null)
		{
			Headline = headline;
			Selected = selected;
			Values = values;
			
			RowSelected = (n) => { };
			
			TableView.Source = new RadioGroupTableViewSource(this);
		}
	}
	
	public class RadioGroupTableViewSource : UITableViewSource
	{
		private RadioGroupTableViewController controller;
		
		public RadioGroupTableViewSource(RadioGroupTableViewController controller) 
		{
			this.controller = controller;
		}
		
		public override int NumberOfSections (UITableView tableView) { return 1; }
		public override int RowsInSection(UITableView tableView, int section) {	return controller.Values.Count; }
		public override string TitleForHeader (UITableView tableView, int section) { return controller.Headline; }
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var section = indexPath.Section;
			var row = indexPath.Row;
			
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "DefaultCell");
			
			cell.TextLabel.Text = string.Format("Cell {0}, value {1}", row, controller.Values[row]);
		
			return cell;
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var section = indexPath.Section;
			var row = indexPath.Row;
			
			Console.WriteLine("Row selected: " + row);
			controller.RowSelected(row);
			
		}
	}
}

