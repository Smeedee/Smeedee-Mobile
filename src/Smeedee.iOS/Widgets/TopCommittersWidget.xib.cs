using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
    [Widget("Top committers", StaticDescription = "See which developer has committed the most code", SettingsType = typeof(TopCommittersConfigTableViewController))]
    public partial class TopCommittersWidget : UITableViewController, IWidget, IToolbarControl
    {
		private TopCommitters model;
		
		// Need to be declared here, or it will be garbage collected
		private UISegmentedControl toolbarControl;
		
        public TopCommittersWidget() : base("TopCommittersWidget", null)
        {
			model = new TopCommitters();
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad ();
			TableView.SeparatorColor = StyleExtensions.tableSeparator;
			TableView.IndicatorStyle = UIScrollViewIndicatorStyle.White;
            Refresh();
        }
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			InvokeOnMainThread(UpdateUI);
		}
		
        public void Refresh()
        {
			InvokeOnMainThread(LoadingIndicator.Instance.StartLoading);
			model.Load(() => {
				InvokeOnMainThread(UpdateUI);
				InvokeOnMainThread(LoadingIndicator.Instance.StopLoading);
			});
        }
		
		private void UpdateUI() {
            TableView.Source = new TopCommitersTableSource(model.Committers);
			TableView.ReloadData();
		}
		
		public UIBarButtonItem ToolbarConfigurationItem()
		{
			var current = model.TimePeriod;
			
			toolbarControl = new UISegmentedControl();
			toolbarControl.InsertSegment("24h", 0, false);
			toolbarControl.InsertSegment("week", 1, false);
			toolbarControl.InsertSegment("month", 2, false);
			toolbarControl.SelectedSegment = (current == TimePeriod.PastDay) ? 0 : (current == TimePeriod.PastWeek) ? 1 : 2;
			toolbarControl.ControlStyle = UISegmentedControlStyle.Bar;
			toolbarControl.Frame = new System.Drawing.RectangleF(0, 10, 130, 30);
			toolbarControl.UserInteractionEnabled = true;
			
			toolbarControl.ValueChanged += delegate {
				switch (toolbarControl.SelectedSegment) {
				case 0:
					model.TimePeriod = TimePeriod.PastDay;
					break;
				case 1:
					model.TimePeriod = TimePeriod.PastWeek;
					break;
				default:
					model.TimePeriod = TimePeriod.PastMonth;
					break;
				}
				ViewWillAppear(true);
			};
			
			return new UIBarButtonItem(toolbarControl);
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
