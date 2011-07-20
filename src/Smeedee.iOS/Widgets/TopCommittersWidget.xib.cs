using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee;

namespace Smeedee.iOS
{
    [Widget("Top committers", StaticDescription = "See which developer has committed the most code")]
    public partial class TopCommittersWidget : UITableViewController, IWidget
    {
		private TopCommitters model;
		
        public TopCommittersWidget() : base("TopCommittersWidget", null)
        {
			model = new TopCommitters();
        }
		
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			TableView.SeparatorColor = UIColor.DarkGray;
			TableView.IndicatorStyle = UIScrollViewIndicatorStyle.White;
            Refresh();
        }
		
        public void Refresh()
        {
			model.Load(() => InvokeOnMainThread(UpdateUI));
        }
		
		private void UpdateUI() {
            TableView.Source = new TopCommitersTableSource(model.Committers);
			TableView.ReloadData();
		}
        
		public string GetDynamicDescription() 
		{
			return model.Description;	
		}
		
        public event EventHandler DescriptionChanged;
        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
    }
    
    public class TopCommitersTableSource : UITableViewSource
    {
		private TableCellFactory cellFactory = new TableCellFactory("TopCommittersTableCellController", typeof(TopCommittersTableCellController));		
        private IEnumerable<Committer> committers;
        
        public TopCommitersTableSource(IEnumerable<Committer> committers)
        {
            this.committers = committers;
        }
        
        public override int NumberOfSections (UITableView tableView)
        {
            return 1;
        }
		
		public override float GetHeightForRow (UITableView tableView, NSIndexPath indexPath)
		{
			return 70f;
		}
        
        public override int RowsInSection (UITableView tableview, int section)
        {
            return committers.Count();
        }
        
        private const string CELL_ID = "TopCommitterCell";
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			var committer = committers.ElementAt(indexPath.Row);
            
            var controller = cellFactory.NewTableCellController(tableView, indexPath) as TopCommittersTableCellController;
            controller.BindDataToCell(committer);
			
            return controller.TableViewCell;
        }
    }
}
