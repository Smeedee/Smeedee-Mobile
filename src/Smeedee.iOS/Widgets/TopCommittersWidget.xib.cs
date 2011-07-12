using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("Top committers widget", 123, DescriptionStatic = "todo")]
    public partial class TopCommittersWidget : UITableViewController, IWidget
    {
        public TopCommittersWidget() : base("TopCommittersWidget", null)
        {
        }
        
        public void Refresh()
        {
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            TableView.Source = new TopCommitersTableSource();
        }
    }
    
    public class TopCommitersTableSource : UITableViewSource
    {
        public override int NumberOfSections (UITableView tableView)
        {
            return 1;
        }
        
        public override int RowsInSection (UITableView tableview, int section)
        {
            return 3;
        }
        
        private const string CELL_ID = "TopCommitterCell";
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_ID) ??
                       new UITableViewCell(UITableViewCellStyle.Subtitle, CELL_ID);
            
            cell.TextLabel.Text = "test";
            
            return cell;
        }
    }
}
