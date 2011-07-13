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
        public BuildStatusWidget() : base("BuildStatusWidget", null)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var builds = GetFakeBuildStatuses();
            TableView.Source = new BuildStatusTableSource(builds);
        }
        
        public void Refresh()
        {
        }
        
		public string GetDynamicDescription() 
		{
			return "";	
		}
        private IEnumerable<BuildStatus> GetFakeBuildStatuses()
        {
            return new [] {
                new BuildStatus("Smeedee.Mobile", BuildSuccessState.Success, "AlexYork", DateTime.Now.AddHours(-3)),
                new BuildStatus("Smeedee.Desktop", BuildSuccessState.Failure, "AlexYork", DateTime.Now.AddHours(-5)),
                new BuildStatus("Smeedee.Web.Services", BuildSuccessState.Success, "AlexYork", DateTime.Now.AddHours(-7))
            };
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
