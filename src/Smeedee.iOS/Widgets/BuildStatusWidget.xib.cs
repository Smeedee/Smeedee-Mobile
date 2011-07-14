using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("Build Status", StaticDescription = "View the status of your builds")]
    public partial class BuildStatusWidget : UITableViewController, IWidget
    {
        private SmeedeeApp app = SmeedeeApp.Instance;
		private IModelService<BuildStatusBoard> service;
		private IBackgroundWorker bgWorker;
		
		private BuildStatusBoard model;
		
        public BuildStatusWidget() : base("BuildStatusWidget", null)
        {
			service = app.ServiceLocator.Get<IModelService<BuildStatusBoard>>();
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			Refresh();
        }
		
		private void GetData()
		{
			model = service.Get();
		}
		
		private void UpdateUI()
		{
            TableView.Source = new BuildStatusTableSource(model.Builds);
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
			return "";	
		}
    }
    
    public class BuildStatusTableSource : UITableViewSource
    {
        private TableCellFactory cellFactory = new TableCellFactory("BuildStatusTableCellController", typeof(BuildStatusTableCellController));
        private IEnumerable<BuildStatus> builds;
        
        public BuildStatusTableSource(IEnumerable<BuildStatus> builds)
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
            var build = builds.ElementAt(indexPath.Row);
            
            var buildStatusCellController = cellFactory.NewTableCellController(tableView, indexPath) as BuildStatusTableCellController;
            buildStatusCellController.BindDataToCell(build);
            return buildStatusCellController.TableViewCell;
        }
        
        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 75;
        }
    }
}
