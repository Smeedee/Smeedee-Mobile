using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class ServerConfigTableViewController : UIViewController
	{
		public ServerConfigTableViewController() : base ("ServerConfigTableViewController", null)
		{
		
		}
		
		public override void ViewDidLoad ()
		{
			this.Title = "Server";
			this.table.Source = new ServerConfigTableSource();
			this.table.ScrollEnabled = false;
			this.button.TitleLabel.Text = "Connect";
		}
	}
	public class ServerConfigTableSource : UITableViewSource
    {
		private Login loginModel;
		private TableCellFactory cellFactory = 
			new TableCellFactory("LabelTextInputTableCellController", typeof(LabelTextInputTableCellController));
		
		public ServerConfigTableSource() : base() {
			loginModel = new Login();
		}
		
        public override int NumberOfSections(UITableView tableView) { return 1; }
        public override int RowsInSection(UITableView tableview, int section) { return 2; }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			var cellController = 
				cellFactory.NewTableCellController(tableView, indexPath) as LabelTextInputTableCellController;
			
			if (indexPath.Row == 0) {
				cellController.BindDataToCell(loginModel.Url);
				cellController.BindActionToReturn((textField) => loginModel.Url = textField.Text);
			}
			else
			{
				cellController.BindDataToCell(loginModel.Key);
				cellController.BindActionToReturn((textField) => loginModel.Key = textField.Text);
			}
			return cellController.TableViewCell;
        }
	}
}

