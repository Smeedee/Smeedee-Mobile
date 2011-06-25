using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iPhone
{
	public partial class TopCommitersScreen : UITableViewController, IWidget
	{
		public TopCommitersScreen() : base("TopCommitersScreen", null)
		{
		}
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			var topCommiters = new TopCommiters();
			topCommiters.Load(() => {
				TableView.ReloadData();
			});
			
			TableView.Source = new TopCommitersTableSource(topCommiters.Commiters);
		}
	}
	
	public class TopCommitersTableSource : UITableViewSource
	{
		private IEnumerable<Commiter> commiters;
		
		public TopCommitersTableSource(IEnumerable<Commiter> commiters)
		{
			this.commiters = commiters;
		}
		
		public override int NumberOfSections(UITableView tableView)
		{
			return 1;
		}
		
		public override int RowsInSection(UITableView tableview, int section)
		{
			return commiters.Count();
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var commiter = commiters.ElementAt(indexPath.Row);
			
			// TODO: add cell reuse/virtualization here!!
			var topCommiterCellController = new TopCommiterTableCell();
			
			NSBundle.MainBundle.LoadNib("TopCommiterTableCell", topCommiterCellController, null);
			topCommiterCellController.BindDataToCell(commiter.Name, 123);
			
			return topCommiterCellController.Cell;
		}
		
		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			return 60f;
		}
	}
}
