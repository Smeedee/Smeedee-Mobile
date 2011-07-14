
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
    [Widget("Latest Commits", StaticDescription = "List of latest commits")]
	public partial class LatestCommits : UITableViewController, IWidget
	{
        private SmeedeeApp app = SmeedeeApp.Instance;
		private IModelService<LatestChangeset> service;
		private IBackgroundWorker bgWorker;
		
		private LatestChangeset model;
		
		public LatestCommits () : base("LatestCommits", null)
		{
			service = app.ServiceLocator.Get<IModelService<LatestChangeset>>();
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
		}
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			TableView.IndicatorStyle = UIScrollViewIndicatorStyle.White;
			Refresh();
        }
		
		private void GetData() 
		{
			model = service.Get();
		}
		
		private void UpdateUI()
		{
			TableView.Source = new LatestCommitsTableSource(model.Changesets);
			TableView.ReloadData();
		}
        
        public void Refresh()
        {
			bgWorker.Invoke(() => {
				GetData();
				InvokeOnMainThread(UpdateUI);
			});
        }
		
		public string GetDynamicDescription() 
		{
			return "TODO";	
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

