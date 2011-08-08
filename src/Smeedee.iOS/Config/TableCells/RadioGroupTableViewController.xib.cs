using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.iOS.Lib;

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
			
			TableView.StyleAsSettingsTable();
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
				AccessoryView = null
			};
			
			if (row == selected)
				cell[row].AccessoryView = BlackAccessoryCheckmark();
			
			cell[row].TextLabel.Text = labels[row];
			cell[row].StyleAsSettingsTableCell();
			
			return cell[row];
		}
		
		public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
		{
			var row = indexPath.Row;
			
			cell[selected].AccessoryView = null;
			cell[row].AccessoryView = BlackAccessoryCheckmark();
			
			selected = row;
			
			Console.WriteLine("Row selected: " + row);
			controller.RowSelected(row);
			
			cell[row].SetSelected(false, true);
		}
		
		public override UIView GetViewForHeader (UITableView tableView, int section)
		{	
			return new ConfigTableSectionHeader(headline);
		}
		
		public override float GetHeightForHeader (UITableView tableView, int section)
		{
			return (float)ConfigTableSectionHeader.Height;
		}
		
		public static UIView BlackAccessoryCheckmark()
		{
			return new UIImageView(UIImage.FromFile("images/checkmark.png")) { Frame = new RectangleF(285, 15, 15, 15) };
		}
	}
}

