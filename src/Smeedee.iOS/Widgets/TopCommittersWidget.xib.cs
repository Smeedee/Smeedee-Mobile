using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.iOS
{
    [Widget("Top committers", 123, DescriptionStatic = "See which developer has committed the most code")]
    public partial class TopCommittersWidget : UITableViewController, IWidget
    {
        private SmeedeeApp app = SmeedeeApp.Instance;
        private IModelService<TopCommitters> service;
            
        public TopCommittersWidget() : base("TopCommittersWidget", null)
        {
            service = app.ServiceLocator.Get<IModelService<TopCommitters>>();
        }
        
        public void Refresh()
        {
        }
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
            
            var args = new Dictionary<string, string>() {
                {"count", "50"},
                {"time", "50"},
            };
            var topCommitters = service.GetSingle(args);
            
            TableView.Source = new TopCommitersTableSource(topCommitters);
        }
    }
    
    public class TopCommitersTableSource : UITableViewSource
    {
        private TopCommitters topCommitters;
        
        public TopCommitersTableSource(TopCommitters topCommitters)
        {
            this.topCommitters = topCommitters;
        }
        
        public override int NumberOfSections (UITableView tableView)
        {
            return 1;
        }
        
        public override int RowsInSection (UITableView tableview, int section)
        {
            return topCommitters.Committers.Count();
        }
        
        private const string CELL_ID = "TopCommitterCell";
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var cell = tableView.DequeueReusableCell(CELL_ID) ??
                       new UITableViewCell(UITableViewCellStyle.Subtitle, CELL_ID);
            
            var committer = topCommitters.Committers.ElementAt(indexPath.Row);
            
            cell.TextLabel.Text = committer.Name;
            
            return cell;
        }
    }
}
