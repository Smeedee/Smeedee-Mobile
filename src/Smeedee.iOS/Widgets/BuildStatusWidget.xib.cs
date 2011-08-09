using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
    [Widget("Build Status", StaticDescription = "View the status of your builds", SettingsType = typeof(BuildStatusConfigTableViewController))]
    public partial class BuildStatusWidget : UITableViewController, IWidget, IToolbarControl
	{
		private BuildStatus model;
		
		// Needs to be declared outside to avoid GC
		private UISegmentedControl toolbarControl;
		
        public BuildStatusWidget() : base("BuildStatusWidget", null)
        {
			model = new BuildStatus();
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			TableView.StyleAsWidgetTable();
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
		
		private void UpdateUI()
		{
            TableView.Source = new BuildStatusTableSource(model);
			TableView.ReloadData();
		}
		
		public UIBarButtonItem ToolbarConfigurationItem()
		{
			var current = model.Ordering;
			
			toolbarControl = new UISegmentedControl();
			toolbarControl.InsertSegment("name", 0, false);
			toolbarControl.InsertSegment("time", 1, false);
			toolbarControl.SelectedSegment = (current == BuildOrder.BuildName) ? 0 : 1;
			toolbarControl.ControlStyle = UISegmentedControlStyle.Bar;
			toolbarControl.Frame = new System.Drawing.RectangleF(0, 10, 100, 30);
			toolbarControl.UserInteractionEnabled = true;
			
			toolbarControl.ValueChanged += delegate {
				switch (toolbarControl.SelectedSegment) {
				case 0:
					model.Ordering = BuildOrder.BuildName;
					break;
				default:
					model.Ordering = BuildOrder.BuildTime;
					break;
				}
				InvokeOnMainThread(UpdateUI);
			};
			
			return new UIBarButtonItem(toolbarControl);
		}
        
		public string GetDynamicDescription() 
		{
			return model.DynamicDescription;	
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
    
    public class BuildStatusTableSource : UITableViewSource
    {   
		private TableCellFactory cellFactory = new TableCellFactory("BuildStatusTableCellController", typeof(BuildStatusTableCellController));
		private BuildStatus model;

        public BuildStatusTableSource(BuildStatus model)
        {
            this.model = model;
        }
        
        public override int NumberOfSections(UITableView tableView) { return model.Builds.Count(); }
        public override int RowsInSection(UITableView tableview, int section) { return 1; }
		     
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var build = model.Builds.ElementAt(indexPath.Section);
            
            var buildStatusCellController = cellFactory.NewTableCellController(tableView, indexPath) as BuildStatusTableCellController;
            buildStatusCellController.BindDataToCell(build);
            return buildStatusCellController.TableViewCell;
        }
        
        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath) { return 60f; }
    }
}
