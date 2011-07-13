
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee;

namespace Smeedee.iOS
{
    [Widget("Latest Commits", DescriptionStatic = "foo")]
	public partial class LatestCommits : UITableViewController, IWidget
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public LatestCommits (IntPtr handle) : base(handle)
		{
			Initialize ();
		}

		[Export("initWithCoder:")]
		public LatestCommits (NSCoder coder) : base(coder)
		{
			Initialize ();
		}

		public LatestCommits () : base("LatestCommits", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}
		
		#endregion
	
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            //var builds = GetFakeBuildStatuses();
            //TableView.Source = new LatestCommitsTableSource(builds);
        }
        
        public void Refresh()
        {
        }
	
	}
	
	
    public class LatestCommitsTableSource : UITableViewSource
    {
        //private TableCellFactory cellFactory = new TableCellFactory("LatestCommitsTableCellController", typeof(BuildStatusTableCellController));
        private IEnumerable<Changeset> builds;
        
        public LatestCommitsTableSource(IEnumerable<Changeset> builds)
        {
            this.builds = builds;
        }
        
        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }
        
        public override int RowsInSection(UITableView tableview, int section)
        {
            return builds.Count();
        }
       
        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        { 
			return null;
			/*
            var build = builds.ElementAt(indexPath.Row);
            
            var buildStatusCellController = cellFactory.NewTableCellController(tableView, indexPath) as BuildStatusTableCellController;
            buildStatusCellController.BindDataToCell(build);
            return buildStatusCellController.TableViewCell;*/
        }
        
        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 75;
        }
    }
}

