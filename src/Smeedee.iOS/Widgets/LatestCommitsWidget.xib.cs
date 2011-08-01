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
			TableView.SeparatorColor = StyleExtensions.tableSeparator;
			TableView.IndicatorStyle = UIScrollViewIndicatorStyle.White;
			Refresh();
        }
		
		private void UpdateUI()
		{
			TableView.Source = new LatestCommitsTableSource(this, model);
			TableView.ReloadData();
			OnDescriptionChanged(new EventArgs());
		}
        
        public void Refresh()
        {
			Console.WriteLine("show loading animation");
			InvokeOnMainThread(WidgetsScreen.StartLoading);
			model.Load(() =>  {
				InvokeOnMainThread(UpdateUI);
				
				Console.WriteLine("hide loading animation");
				InvokeOnMainThread(WidgetsScreen.StopLoading);
			});
        }
		
		public void LoadMore()
		{
			InvokeOnMainThread(UpdateUI);
		}
		
		public string GetDynamicDescription() 
		{
			return model.DynamicDescription;	
		}
		
		public DateTime LastRefreshTime()
		{
			return DateTime.Now;	
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
		
		private LatestCommits model;
		private LatestCommitsWidget controller;
        
        private const float CELL_PADDING = 15f;
        
        public LatestCommitsTableSource(LatestCommitsWidget controller, LatestCommits model)
        {
            this.model = model;
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
			return 30;
		}
		
		public override float GetHeightForFooter (UITableView tableView, int section) {	return 0; }
		
        public override int NumberOfSections(UITableView tableView) { return 2; }
        public override int RowsInSection(UITableView tableview, int section) { return (section == 0) ? model.Commits.Count() : 1; }
       
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			if (indexPath.Section == 0) {
	            var commit = model.Commits.ElementAt(indexPath.Row);
	            
	            var controller = cellFactory.NewTableCellController(tableView, indexPath) as CommitTableCellController;
	            controller.BindDataToCell(commit, model.HighlightEmpty);
	            
	            return controller.TableViewCell;
			}
			else
			{
				var buttonController = buttonCellFactory.NewTableCellController(tableView, indexPath) as LatestCommitsLoadMoreTableCellController;
				return buttonController.TableViewCell;
			}
        }
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 1)
			{
				model.LoadMore(() => controller.LoadMore());	
			}
		}
    }
}
