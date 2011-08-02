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
		public ServerConfigTableViewController() : base ("ServerConfigTableViewController", null) { }
		
		public override void ViewDidLoad ()
		{
			LoginAction = (str) => { };
			Title = "Smeedee Server";
			
			table.Source = new ServerConfigTableSource(this);
			table.SeparatorColor = UIColor.Black;
			table.ScrollEnabled = false;
		}
		
		public Action<string> LoginAction
		{
			get; set;
		}
	}
	
	public class ServerConfigTableSource : UITableViewSource
    {
		private Login loginModel;
		private ServerConfigTableViewController controller;
		
		private LabelTextInputTableCellController serverUrl;
		private LabelTextInputTableCellController userKey;
		
		private TableCellFactory cellFactory = 
			new TableCellFactory("LabelTextInputTableCellController", typeof(LabelTextInputTableCellController));
		
		private UITableViewCell buttonCell;
		
		public ServerConfigTableSource(ServerConfigTableViewController controller) : base() 
		{
			this.controller = controller;
			loginModel = new Login();
		}
		
        public override int NumberOfSections(UITableView tableView) { return 2; }
        public override int RowsInSection(UITableView tableview, int section) { return 2 - section; }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			var cellController = 
				cellFactory.NewTableCellController(tableView, indexPath) as LabelTextInputTableCellController;
			
			switch (indexPath.Section) {
			case 0:
				if (indexPath.Row == 0) {
					cellController.BindDataToCell(string.IsNullOrEmpty(loginModel.Url) ? Login.DefaultSmeedeeUrl : loginModel.Url);
					cellController.BindActionToReturn((textField) => loginModel.Url = textField.Text);
					cellController.TextInput.Placeholder = "url";
					serverUrl = cellController;
				}
				else
				{
					cellController.BindDataToCell(string.IsNullOrEmpty(loginModel.Key) ? Login.DefaultSmeedeeKey : loginModel.Key);
					cellController.BindActionToReturn((textField) => loginModel.Key = textField.Text);
					cellController.TextInput.Placeholder = "key";
					userKey = cellController;
				}
				return cellController.TableViewCell;
			default:
				buttonCell = new UITableViewCell();
				buttonCell.TextLabel.Text = "Connect";
				buttonCell.StyleAsSettingsTableCell();
				buttonCell.TextLabel.TextAlignment = UITextAlignment.Center;
				return buttonCell;
			}
        }
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			if (indexPath.Section == 0) return;
			
			var url = serverUrl.TextInput.Text;
			var key = userKey.TextInput.Text;
			
			Console.WriteLine(string.Format("Logging in with {0} : {1}", url, key));
			WidgetsScreen.StartLoading();
			
			new Login().StoreAndValidate(url, key, (str) => {
				
				Console.WriteLine(string.Format("Response from server: {0}", str));
				
				InvokeOnMainThread(() => {
					buttonCell.SetSelected(false, true);
				});
				WidgetsScreen.StopLoading();
				controller.LoginAction(str);
			});
		}
	}
}

