using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.iPhone
{
    [Widget("Top Commiters", 2, DescriptionStatic = "Shows the most active committers")]
    public partial class TopCommitersScreen : UITableViewController, IWidget
    {
        public TopCommitersScreen() : base("TopCommitersScreen", null)
        {
        }
        
        private TopCommitters topCommitters;
        private IModelService<TopCommitters> service = SmeedeeApp.Instance.ServiceLocator.Get<IModelService<TopCommitters>>();
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var tableSource = new TopCommitersTableSource();
            TableView.Source = tableSource;
			
			topCommitters = service.GetSingle(new Dictionary<string, string>());
            InvokeOnMainThread(() => {
                tableSource.Committers = topCommitters.Committers;
                TableView.ReloadData();
            });
        }
    }
    
    public class TopCommitersTableSource : UITableViewSource
    {
        public IEnumerable<Committer> Committers { get; set; }
        
        public TopCommitersTableSource()
        {
            Committers = new List<Committer>();
        }
        
        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }
        
        public override int RowsInSection(UITableView tableview, int section)
        {
            return Committers.Count();
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var committer = Committers.ElementAt(indexPath.Row);
            
            // TODO: add cell reuse/virtualization here!!
            var topCommiterCellController = new TopCommiterTableCell();
            
            NSBundle.MainBundle.LoadNib("TopCommiterTableCell", topCommiterCellController, null);
            topCommiterCellController.BindDataToCell(committer.Name, 123);
            
            return topCommiterCellController.Cell;
        }
        
        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath)
        {
            return 60f;
        }
    }
}
