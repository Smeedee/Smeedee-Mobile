using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
    public partial class ConfigurationScreen : UINavigationController
    {
        #region Constructors

        public ConfigurationScreen(IntPtr handle) : base(handle)
        {
        }

        [Export ("initWithCoder:")]
        public ConfigurationScreen(NSCoder coder) : base(coder)
        {
        }

        public ConfigurationScreen() : base("ConfigurationScreen", null)
        {
        }

        #endregion
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var configTableController = new ConfigurationTableViewController();
            PushViewController(configTableController, false);
        }
    }
    
    public class ConfigurationTableViewController : UITableViewController
    {
        public ConfigurationTableViewController()
            : base(UITableViewStyle.Grouped)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            Title = "Configuration";
            
            TableView.Source = new ConfigurationTableSource();
        }
    }
    
    public class ConfigurationTableSource : UITableViewSource
    {
        public override int NumberOfSections(UITableView tableView)
        {
            return 2;
        }
        
        public override string TitleForHeader(UITableView tableView, int section)
        {
            return "Section header " + section; 
        }
        
        public override int RowsInSection(UITableView tableview, int section)
        {
            return 3;
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell("CellID") ??
                       new UITableViewCell(UITableViewCellStyle.Subtitle, "CellID");
            
            cell.TextLabel.Text = "Row " + indexPath.Row;
            cell.DetailTextLabel.Text = "Section " + indexPath.Section;
            
            return cell;
        }
        
        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine("Selected row " + indexPath.Section + " / " + indexPath.Row);
        }
    }
}
