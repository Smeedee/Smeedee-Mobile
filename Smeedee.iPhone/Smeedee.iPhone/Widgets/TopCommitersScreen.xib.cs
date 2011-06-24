using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iPhone
{
	public partial class TopCommitersScreen : UITableViewController
	{
		public TopCommitersScreen() : base("TopCommitersScreen", null)
		{
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			TableView.Source = new TopCommitersTableSource();
		}
	}
	
	public class TopCommitersTableSource : UITableViewSource
	{
		public override int NumberOfSections(UITableView tableView)
		{
			return 1;
		}
		
		public override int RowsInSection(UITableView tableview, int section)
		{
			return 3;
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			// TODO: add cell reuse/virtualization here!!
			var topCommiterCellController = new TopCommiterTableCell();
			
			NSBundle.MainBundle.LoadNib("TopCommiterTableCell", topCommiterCellController, null);
			topCommiterCellController.BindDataToCell("User " + indexPath.Row, 27);
			
			return topCommiterCellController.Cell;
		}
		
		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 60f;
		}
	}
}
