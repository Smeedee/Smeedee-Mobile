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
		}
	}
	
	public class MainConfigTableSource : UITableViewSource
    {
		private MainConfigTableViewController controller;
		private Login loginModel;
		
		private UITextField urlTextField;
		private UITextField keyTextField;
		
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
			switch (indexPath.Section) {
			case 0:
				if (indexPath.Row == 0) {
					var cell = new UITableViewCell();
					cell.TextLabel.Text = "Server";
					
					urlTextField = new UITextField(cell.Frame);
					urlTextField.Text = loginModel.Url;
					
					urlTextField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					urlTextField.LeftView = new UIView(new RectangleF(0, 0, 80, 31)); 
					urlTextField.LeftViewMode = UITextFieldViewMode.Always;
					urlTextField.ReturnKeyType = UIReturnKeyType.Done;
					urlTextField.ShouldReturn = delegate { 
						loginModel.Url = urlTextField.Text;
						urlTextField.ResignFirstResponder();
						return true; 
					};
					
					cell.AddSubview(urlTextField);
					return cell;
				}
				else {
					var cell = new UITableViewCell();
					cell.TextLabel.Text = "Key";
					
					keyTextField = new UITextField(cell.Frame);
					keyTextField.Text = loginModel.Key;
					
					keyTextField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					keyTextField.LeftView = new UIView(new RectangleF(0,0, 80, 31)); 
					keyTextField.LeftViewMode = UITextFieldViewMode.Always;
					keyTextField.ReturnKeyType = UIReturnKeyType.Done;
					keyTextField.ShouldReturn = delegate { 
						loginModel.Key = keyTextField.Text;
						keyTextField.ResignFirstResponder();
						return true; 
					};
					
					cell.AddSubview(keyTextField);
					return cell;
				}
			default:
				var widget = SmeedeeApp.Instance.AvailableWidgets.ElementAt(indexPath.Row);
				
	            var cell = tableView.DequeueReusableCell("CellID") ??
	                       new UITableViewCell(UITableViewCellStyle.Subtitle, "CellID");
	            
	            cell.TextLabel.Text = widget.Name;
	            cell.DetailTextLabel.Text = widget.StaticDescription;
            	cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
	            
	            return cell;
			}
        }
		
        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            Console.WriteLine("Selected row " + indexPath.Section + " / " + indexPath.Row);
			
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

