using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee;

namespace Smeedee.iOS
{
    [Widget("Top committers", StaticDescription = "See which developer has committed the most code", SettingsType = typeof(TopCommittersConfigTableViewController))]
    public partial class TopCommittersWidget : UITableViewController, IWidget, IToolbarControl
    {
		private TopCommitters model;
		
        public TopCommittersWidget() : base("TopCommittersWidget", null)
        {
			model = new TopCommitters();
        }
		
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
			//TableView.SeparatorStyle = UITableViewCellSeparatorStyle.None;
			TableView.SeparatorColor = StyleExtensions.tableSeparator;
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
		
		public UIBarButtonItem ToolbarConfigurationItem()
		{
			/*
			var item = new UIBarButtonItem("heya", UIBarButtonItemStyle.Bordered, null);
			
			return item;*/
			
			var control = new UISegmentedControl();
			control.InsertSegment("24h", 0, false);
			control.InsertSegment("week", 1, false);
			control.InsertSegment("month", 2, false);
			control.SelectedSegment = 0;
			control.ControlStyle = UISegmentedControlStyle.Bar;
			control.TintColor = UIColor.Black;
			control.BackgroundColor = StyleExtensions.transparent;
			
			control.Frame = new System.Drawing.RectangleF(0, 10, 130, 30);
			
			var item = new UIBarButtonItem(control);
			return item;
		}
        
		public string GetDynamicDescription() 
		{
			return model.Description;	
		}
		
		public DateTime LastRefreshTime()
		{
			return DateTime.Now;	
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
			return 64f;
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
            controller.BindDataToCell(committer, committer.Commits / (float)committers.First().Commits);
			
            return controller.TableViewCell;
        }
    }
}
