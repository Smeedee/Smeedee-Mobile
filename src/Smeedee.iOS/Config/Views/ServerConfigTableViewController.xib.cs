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
		// Needed for instantiation in AppDelegateIPhone
		public ServerConfigTableViewController(IntPtr handle) : base (handle) { }
		
		public ServerConfigTableViewController() : base ("ServerConfigTableViewController", null)
		{
		}
		
		public override void ViewDidLoad ()
		{
			Title = " Smeedee Server";
			table.Source = new ServerConfigTableSource();
			table.ScrollEnabled = false;
			
			button.TitleLabel.Text = "Connect";
			button.StyleAsGreyButton();
			
			button.TouchUpInside += delegate {
				var serverUrl = ((ServerConfigTableSource)table.Source).GetServerUrl();
				var userKey = ((ServerConfigTableSource)table.Source).GetUserKey();
				
				new Login().StoreAndValidate(serverUrl, userKey, (str) => 
				{
					InvokeOnMainThread(() => 
					{
						if (str == Login.ValidationSuccess) button.SetTitleColor(UIColor.Green, UIControlState.Normal);
						else button.SetTitleColor(UIColor.Red, UIControlState.Normal);
						button.SetTitle(str, UIControlState.Normal);
					});
				});
			};
		}
		
	}
	public class ServerConfigTableSource : UITableViewSource
    {
		private Login loginModel;
		private LabelTextInputTableCellController serverUrl;
		private LabelTextInputTableCellController userKey;
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
				cellController.TextInput.Placeholder = "http://smeedee.someurl.com";
				serverUrl = cellController;
				
			}
			else
			{
				cellController.BindDataToCell(loginModel.Key);
				cellController.BindActionToReturn((textField) => loginModel.Key = textField.Text);
				cellController.TextInput.Placeholder = "password";
				userKey = cellController;
			}
			return cellController.TableViewCell;
        }
		public string GetServerUrl() {
			return serverUrl.TextInput.Text;
		}
		public string GetUserKey() {
			return userKey.TextInput.Text;
		}
	}
}

