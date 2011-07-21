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
		private Login loginModel;
		
		public MainConfigTableViewController() : base("MainConfigTableViewController", null) 
		{ 
			loginModel = new Login();
			this.Title = "Settings";
			this.TableView.Source = new MainConfigTableSource(this);
		}
		
		public string UrlSetting
		{
			get { return loginModel.Url; }
			set { loginModel.Url = value; }
		}
		
		public string KeySetting
		{
			get { return loginModel.Key; }
			set { loginModel.Key = value; }
		}
	}
	
	public class MainConfigTableSource : UITableViewSource
    {
		private MainConfigTableViewController controller;
		
		private UITextField urlTextField;
		private UITextField keyTextField;
		
		public MainConfigTableSource(MainConfigTableViewController controller) : base() {
			this.controller = controller;
		}
		
        public override int NumberOfSections(UITableView tableView)
        {
            return 2;
        }
        
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
					urlTextField.Text = controller.UrlSetting;
					
					urlTextField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					urlTextField.LeftView = new UIView(new RectangleF(0, 0, 80, 31)); 
					urlTextField.LeftViewMode = UITextFieldViewMode.Always;
					urlTextField.ReturnKeyType = UIReturnKeyType.Done;
					urlTextField.ShouldReturn = delegate { 
						controller.UrlSetting = urlTextField.Text;
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
					keyTextField.Text = controller.KeySetting;
					
					keyTextField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					keyTextField.LeftView = new UIView(new RectangleF(0,0, 80, 31)); 
					keyTextField.LeftViewMode = UITextFieldViewMode.Always;
					keyTextField.ReturnKeyType = UIReturnKeyType.Done;
					keyTextField.ShouldReturn = delegate { 
						controller.KeySetting = keyTextField.Text;
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
			
			var name = SmeedeeApp.Instance.AvailableWidgets.ElementAt(indexPath.Row).Name;
			
			// TODO: Want to instantiate a class of type found in the WidgetModel
			
			controller.NavigationController.PushViewController(new TopCommittersWidgetTableViewController(), true);
			
			/*
			switch (name) {
			case "Top committers":
				controller.NavigationController.PushViewController(new ConfigurationTableViewController(new TopComittersTableSource(), name), true);
				break;
			case "Build Status":
				controller.NavigationController.PushViewController(new ConfigurationTableViewController(new BuildStatusTableSource(), name), true);
				break;
			case "Latest Commits":
				controller.NavigationController.PushViewController(new ConfigurationTableViewController(new LatestCommitsTableSource(), name), true);
				break;
			case "Working days left":
				controller.NavigationController.PushViewController(new ConfigurationTableViewController(new WorkingDaysLeftTableSource(), name), true);
				break;
			}*/
        }
		
	}
}

