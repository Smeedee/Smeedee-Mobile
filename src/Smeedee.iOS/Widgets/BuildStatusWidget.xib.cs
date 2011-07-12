using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("Build Status", 123456, DescriptionStatic = "View the status of your builds")]
    public partial class BuildStatusWidget : UITableViewController, IWidget
    {
        public BuildStatusWidget() : base("BuildStatusWidget", null)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            TableView.Source = new BuildStatusTableSource();
        }
        
        public void Refresh()
        {
        }
    }
    
    public class BuildStatusTableSource : UITableViewSource
    {
        private TableCellFactory cellFactory = new TableCellFactory("BuildStatusTableCellController", typeof(BuildStatusTableCellController));
        
        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }
        
        public override int RowsInSection(UITableView tableview, int section)
        {
            return 4;
        }
        
        public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
        {
            var buildStatusCellController = cellFactory.NewTableCellController(tableView, indexPath) as BuildStatusTableCellController;
            buildStatusCellController.BindDataToCell("Smeedee.Mobile", DateTime.Now, "Broked!!12!!1!!");
            return buildStatusCellController.TableViewCell;
        }
        
        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 75;
        }
    }
}
