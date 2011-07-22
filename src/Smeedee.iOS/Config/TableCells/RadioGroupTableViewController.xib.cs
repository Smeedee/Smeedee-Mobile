using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public partial class RadioGroupTableViewController : UITableViewController
	{
		public string Headline { get; private set; }
		public int Selected { get; set; }
		
		public Action<int> RowSelected { get; set; }
		
		public RadioGroupTableViewController(string headline, IList<string> labels) : base ("RadioGroupTableViewController", null)
		{
			RowSelected = (n) => { };
			
			TableView.Source = new RadioGroupTableViewSource(this, headline, labels);
		}
	}
	
	public class RadioGroupTableViewSource : UITableViewSource
	{
		private RadioGroupTableViewController controller;
		
		private IList<string> labels;
		private string headline;
		
		public RadioGroupTableViewSource(RadioGroupTableViewController controller, string headline, IList<string> labels) 
		{
			this.controller = controller;
			this.headline = headline;
			this.labels = labels;
		}
		
		public override int NumberOfSections (UITableView tableView) { return 1; }
		public override int RowsInSection(UITableView tableView, int section) {	return labels.Count; }
		public override string TitleForHeader (UITableView tableView, int section) { return headline; }
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var section = indexPath.Section;
			var row = indexPath.Row;
			
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "DefaultCell") {
				Accessory = UITableViewCellAccessory.None
			};
			
			cell.TextLabel.Text = labels[row]; //string.Format("Cell {0}, value {1}", row, controller.Values[row]);
		
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

