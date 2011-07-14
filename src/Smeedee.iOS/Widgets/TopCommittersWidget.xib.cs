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
        private SmeedeeApp app = SmeedeeApp.Instance;
		private IModelService<TopCommitters> service;
		private IBackgroundWorker bgWorker;
		
		private TopCommitters model;
		
        public TopCommittersWidget() : base("TopCommittersWidget", null)
        {
            service = app.ServiceLocator.Get<IModelService<TopCommitters>>();
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
        }
		
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			TableView.SeparatorColor = UIColor.DarkGray;
			TableView.IndicatorStyle = UIScrollViewIndicatorStyle.White;
            Refresh();
        }
		
		private void GetData() {
			model = service.Get();
		}
		
		private void UpdateUI() {
            TableView.Source = new TopCommitersTableSource(model);
			TableView.ReloadData();
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
    
    public class TopCommitersTableSource : UITableViewSource
    {
		private TableCellFactory cellFactory = new TableCellFactory("TopCommittersTableCellController", typeof(TopCommittersTableCellController));		
        private TopCommitters topCommitters;
        
        public TopCommitersTableSource(TopCommitters topCommitters)
        {
            this.topCommitters = topCommitters;
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
            return topCommitters.Committers.Count();
        }
        
        private const string CELL_ID = "TopCommitterCell";
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			var committer = topCommitters.Committers.ElementAt(indexPath.Row);
            
            var controller = cellFactory.NewTableCellController(tableView, indexPath) as TopCommittersTableCellController;
            controller.BindDataToCell(committer);
			            
            return controller.TableViewCell;
        }
    }
}
