using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("Build Status", StaticDescription = "View the status of your builds", SettingsType = typeof(BuildStatusConfigTableViewController))]
    public partial class BuildStatusWidget : UITableViewController, IWidget
	{
		private BuildStatus model;
		
        public BuildStatusWidget() : base("BuildStatusWidget", null)
        {
			model = new BuildStatus();
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			TableView.SeparatorColor = UIColor.DarkGray;
			TableView.IndicatorStyle = UIScrollViewIndicatorStyle.White;
			Refresh();
        }
		
        public void Refresh()
        {
			model.Load(() => InvokeOnMainThread(UpdateUI));
        }
		
		private void UpdateUI()
		{
			Console.WriteLine("Updating UI");
            TableView.Source = new BuildStatusTableSource(model);
			TableView.ReloadData();
		}
        
		public string GetDynamicDescription() 
		{
			return model.DynamicDescription;	
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
        
        public override int NumberOfSections(UITableView tableView) { return 1; }
        public override int RowsInSection(UITableView tableview, int section) { return model.Builds.Count(); }
		     
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            var build = model.Builds.ElementAt(indexPath.Row);
            
            var buildStatusCellController = cellFactory.NewTableCellController(tableView, indexPath) as BuildStatusTableCellController;
            buildStatusCellController.BindDataToCell(build);
            return buildStatusCellController.TableViewCell;
        }
        
        public override float GetHeightForRow(UITableView tableView, NSIndexPath indexPath) { return 70f; }
    }
}
