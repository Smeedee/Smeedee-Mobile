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
		
		private TopCommiters topCommiters = new TopCommiters();
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			var tableSource = new TopCommitersTableSource();
			TableView.Source = tableSource;
			
			topCommiters.Load(() => {
				InvokeOnMainThread(() => {
					tableSource.Commiters = topCommiters.Commiters;
					TableView.ReloadData();
				});
			});
		}
	}
	
	public class TopCommitersTableSource : UITableViewSource
	{
		public IEnumerable<Commiter> Commiters { get; set; }
		
		public TopCommitersTableSource()
		{
			Commiters = new List<Commiter>();
		}
		
		public override int NumberOfSections(UITableView tableView)
		{
			return 1;
		}
		
		public override int RowsInSection(UITableView tableview, int section)
		{
			return Commiters.Count();
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			var commiter = Commiters.ElementAt(indexPath.Row);
			
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
