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
            var cell = new UITableViewCell(UITableViewCellStyle.Default, "CellID");
            cell.TextLabel.Text = "Cell " + indexPath.Row;
            return cell;
        }
    }
}
