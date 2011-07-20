using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee;

namespace Smeedee.iOS
{
    [Widget("Latest Commits", StaticDescription = "List of latest commits")]
	public partial class LatestCommits : UITableViewController, IWidget
	{
		private Smeedee.Model.LatestCommits model;
		
		public LatestCommits() : base("LatestCommits", null)
		{
			model = new Smeedee.Model.LatestCommits();
		}
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			TableView.SeparatorColor = UIColor.DarkGray;
			TableView.IndicatorStyle = UIScrollViewIndicatorStyle.White;
			Refresh();
        }
		
		private void UpdateUI()
		{
			TableView.Source = new LatestCommitsTableSource(model.Commits);
			TableView.ReloadData();
		}
        
        public void Refresh()
        {
			model.Load(() => InvokeOnMainThread(UpdateUI));
        }
		
		public string GetDynamicDescription() 
		{
			return "";	
		}
		
        public event EventHandler DescriptionChanged;
        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
	}
	
    public class LatestCommitsTableSource : UITableViewSource
    {
		private TableCellFactory cellFactory = new TableCellFactory("CommitTableCellController", typeof(CommitTableCellController));
        private List<Commit> commits;
        
        private const float CELL_PADDING = 20f;
        
        public LatestCommitsTableSource(IEnumerable<Commit> commits)
        {
            this.commits = commits.ToList();
        }
		
		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			var cell = GetCell(tableView, indexPath);
			var rowHeight = 0f;
			foreach (var view in cell.ContentView.Subviews) {
				if (view is UILabel || view is UITextView) {
					var height = view.SizeThatFits(new SizeF(240f, float.MaxValue)).Height;
					rowHeight += height;
				}
			}
			return rowHeight + CELL_PADDING;
		}
        
        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }
        
        public override int RowsInSection(UITableView tableview, int section)
        {
            return commits.Count();
        }
       
		private const string CELL_ID = "LatestCommitsCell";
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {           
            var commit = commits[indexPath.Row];
            
            var controller = cellFactory.NewTableCellController(tableView, indexPath) as CommitTableCellController;
            controller.BindDataToCell(commit);
            
            return controller.TableViewCell;
        }
    }
}
