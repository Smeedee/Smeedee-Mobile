using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class MainConfigTableViewController : UITableViewController
	{
		public MainConfigTableViewController() : base("MainConfigTableViewController", null) 
		{ 
			this.Title = "Settings";
			this.TableView.Source = new MainConfigTableSource(this);
			
			TableView.StyleAsSettingsTable();
		}
	}
	
	public class MainConfigTableSource : UITableViewSource
    {
		private MainConfigTableViewController controller;
		private Login loginModel;
		private TableCellFactory cellFactory = 
			new TableCellFactory("LabelTextInputTableCellController", typeof(LabelTextInputTableCellController));
		
		public MainConfigTableSource(MainConfigTableViewController controller) : base() {
			this.controller = controller;
			loginModel = new Login();
		}
		
        public override int NumberOfSections(UITableView tableView) { return 2; }
		
        public override string TitleForHeader(UITableView tableView, int section)
        {
			return (section == 0) ? "Smeedee server" : "Widgets";
        }
		
        public override int RowsInSection(UITableView tableview, int section)
        {
			return (section == 0) ? 2 : SmeedeeApp.Instance.AvailableWidgets.Count;
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			switch (indexPath.Section) 
			{
			case 0:
				var cellController = 
					cellFactory.NewTableCellController(tableView, indexPath) as LabelTextInputTableCellController;
				
				if (indexPath.Row == 0) {
					cellController.BindDataToCell("Url", loginModel.Url);
					cellController.BindActionToReturn((textField) => loginModel.Url = textField.Text);
				}
				else
				{
					cellController.BindDataToCell("Key", loginModel.Key);
					cellController.BindActionToReturn((textField) => loginModel.Key = textField.Text);
				}
				return cellController.TableViewCell;
			
			default:
				var widget = SmeedeeApp.Instance.AvailableWidgets.ElementAt(indexPath.Row);
				
	            var cell = tableView.DequeueReusableCell("CellID") ??
	                       new UITableViewCell(UITableViewCellStyle.Subtitle, "CellID");
	            
	            cell.TextLabel.Text = widget.Name;
	            cell.DetailTextLabel.Text = widget.StaticDescription;
            	cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
	            cell.StyleAsSettingsTableCell();
				
	            return cell;
			}
        }
		
        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
			if (indexPath.Section != 1) return;
			
			var widgetModel = SmeedeeApp.Instance.AvailableWidgets.ElementAt(indexPath.Row);
			if (widgetModel.SettingsType != null) 
			{
				var settingsControllerInstance = Activator.CreateInstance(widgetModel.SettingsType) as UIViewController;
				controller.NavigationController.PushViewController(settingsControllerInstance, true);
			}
        }
	}
}

