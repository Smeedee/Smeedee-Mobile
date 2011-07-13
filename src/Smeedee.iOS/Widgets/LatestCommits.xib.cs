
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee;

namespace Smeedee.iOS
{
    [Widget("Latest Commits", StaticDescription = "List of latest commits from <source>")]
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
		
		private IModelService<Changeset> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<Changeset>>();
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
			var data = service.Get(null);
				
			TableView.Source = new LatestCommitsTableSource(data);
			
        }
        
        public void Refresh()
        {
        }
		
		public string GetDynamicDescription() 
		{
			return "";	
		}
	
	}
	
    public class LatestCommitsTableSource : UITableViewSource
    {
        private IEnumerable<Changeset> changesets;
        
        public LatestCommitsTableSource(IEnumerable<Changeset> changesets)
        {
            this.changesets = changesets;
        }
        
        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }
        
        public override int RowsInSection(UITableView tableview, int section)
        {
            return changesets.Count();
        }
       
		private const string CELL_ID = "LatestCommitsCell";
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_ID) ??
                       new UITableViewCell(UITableViewCellStyle.Subtitle, CELL_ID);
            
            var changeset = changesets.ElementAt(indexPath.Row);
            
            cell.TextLabel.Text = changeset.User;
			cell.DetailTextLabel.Text = changeset.Message;
            
            return cell;
        }
    }
}

