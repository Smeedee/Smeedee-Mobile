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
    [Widget("Latest Commits", StaticDescription = "List of latest commits", SettingsType = typeof(LatestCommitsConfigTableViewController))]
	public partial class LatestCommitsWidget : UITableViewController
	{
		private LatestCommits model;
		
		public LatestCommitsWidget() : base("LatestCommits", null)
		{
			model = new LatestCommits();
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
			TableView.Source = new LatestCommitsTableSource(this, model.Commits);
			TableView.ReloadData();
			OnDescriptionChanged(new EventArgs());
		}
        
        public void Refresh()
        {
			model.Load(() => InvokeOnMainThread(UpdateUI));
        }
		
		public void LoadMore()
		{
			model.LoadMore(() => InvokeOnMainThread(UpdateUI));	
		}
		
		public string GetDynamicDescription() 
		{
			return "TODO";	
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
		private TableCellFactory cellFactory =	new TableCellFactory("CommitTableCellController", typeof(CommitTableCellController));
        private TableCellFactory buttonCellFactory = new TableCellFactory("LatestCommitsLoadMoreTableCellController", typeof(LatestCommitsLoadMoreTableCellController));
		
		private List<Commit> commits;
		private LatestCommitsWidget controller;
        
        private const float CELL_PADDING = 20f;
        
        public LatestCommitsTableSource(LatestCommitsWidget controller, IEnumerable<Commit> commits)
        {
            this.commits = commits.ToList();
			this.controller = controller;
        }
		
		public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
		{
			var row = indexPath.Row;
			if (row < commits.Count) {
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
			return 50;
		}
        
        public override int NumberOfSections(UITableView tableView) { return 1; }
        public override int RowsInSection(UITableView tableview, int section) { return commits.Count() + 1; }
       
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			var row = indexPath.Row;
			if (row < commits.Count()) {
	            var commit = commits[indexPath.Row];
	            
	            var controller = cellFactory.NewTableCellController(tableView, indexPath) as CommitTableCellController;
	            controller.BindDataToCell(commit);
	            
	            return controller.TableViewCell;
			}
			else
			{
				var buttonController = buttonCellFactory.NewTableCellController(tableView, indexPath) as LatestCommitsLoadMoreTableCellController;
				
				buttonController.BindAction(() => controller.LoadMore());
				
				return buttonController.TableViewCell;
			}
        }
    }
}
