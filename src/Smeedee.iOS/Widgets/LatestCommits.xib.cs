
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee;
using System.Drawing;

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
		
		private IModelService<LatestChangeset> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<LatestChangeset>>();
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
			var model = service.Get();
				
			TableView.Source = new LatestCommitsTableSource(model.Changesets);
			
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
		private TableCellFactory cellFactory = new TableCellFactory("CommitTableCellController", typeof(CommitTableCellController));
        private List<Changeset> changesets;
        
        public LatestCommitsTableSource(IEnumerable<Changeset> changesets)
        {
            this.changesets = changesets.ToList();
        }
		
		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			var cell = GetCell(tableView, indexPath);
			var rowHeight = 0f;
			foreach (var view in cell.ContentView.Subviews) {
				if (view is UILabel || view is UITextView) {
					var height = view.SizeThatFits(new SizeF(240, float.MaxValue)).Height;
					rowHeight += height;
				}
			}
			return rowHeight+20;
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
            var changeset = changesets[indexPath.Row];
            
            var controller = cellFactory.NewTableCellController(tableView, indexPath) as CommitTableCellController;
            controller.BindDataToCell(changeset);
            
            return controller.TableViewCell;
        }
    }
}

