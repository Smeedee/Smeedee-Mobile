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
	public partial class LatestCommitsWidget : UITableViewController, IWidget
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
			return model.DynamicDescription;	
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
			var section = indexPath.Section;
			if (section == 0) {
				var cell = GetCell(tableView, indexPath);
				
				var message = cell.ContentView.Subviews.ElementAt(3);
				var height = 50 + Math.Max(message.SizeThatFits(new SizeF(240, float.MaxValue)).Height - 20, 0);
				
				return height + CELL_PADDING;
			}
			return 50;
		}
        
        public override int NumberOfSections(UITableView tableView) { return 2; }
        public override int RowsInSection(UITableView tableview, int section) { return (section == 0) ? commits.Count() : 1; }
       
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			var section = indexPath.Section;
			var row = indexPath.Row;
			if (section == 0) {
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
