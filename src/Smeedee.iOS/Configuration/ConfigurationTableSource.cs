using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS.Configuration
{
	public class ConfigurationTableSource : UITableViewSource
    {
		private SmeedeeApp app = SmeedeeApp.Instance;
		private UINavigationController controller;
		protected IPersistenceService persister = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
		
		private UITextField urlTextField;
		private UITextField keyTextField;
		
		public ConfigurationTableSource(UINavigationController controller) : base() {
			this.controller = controller;
		}
		
        public override int NumberOfSections(UITableView tableView)
        {
            return 2;
        }
        
        public override string TitleForHeader(UITableView tableView, int section)
        {
			switch (section) {
			case 0:
				return "Smeedee server";
			default:
				return "Widgets";
			}
        }
        
        public override int RowsInSection(UITableView tableview, int section)
        {
			switch (section) {
			case 0:
				return 2;
			default:
				return app.AvailableWidgets.Count;
			}
        }
        
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
			switch (indexPath.Section) {
			case 0:
				if (indexPath.Row == 0) {
					var cell = new UITableViewCell();
					cell.TextLabel.Text = "Server";
					
					urlTextField = new UITextField(cell.Frame);
					urlTextField.Text = persister.Get<string>("Server.Url", null);
					
					urlTextField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					urlTextField.LeftView = new UIView(new RectangleF(0, 0, 80, 31)); 
					urlTextField.LeftViewMode = UITextFieldViewMode.Always;
					urlTextField.ReturnKeyType = UIReturnKeyType.Done;
					urlTextField.ShouldReturn = delegate { 
						persister.Save("Server.Url", urlTextField.Text);
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
					keyTextField.Text = persister.Get<string>("Server.Key", null);
					
					keyTextField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					keyTextField.LeftView = new UIView(new RectangleF(0,0, 80, 31)); 
					keyTextField.LeftViewMode = UITextFieldViewMode.Always;
					keyTextField.ReturnKeyType = UIReturnKeyType.Done;
					keyTextField.ShouldReturn = delegate { 
						persister.Save("Server.Key", keyTextField.Text);
						keyTextField.ResignFirstResponder();
						return true; 
					};
					
					cell.AddSubview(keyTextField);
					return cell;
				}
			default:
				var widget = app.AvailableWidgets.ElementAt(indexPath.Row);
				
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
			
			var name = app.AvailableWidgets.ElementAt(indexPath.Row).Name;
			
			switch (name) {
			case "Top committers":
				controller.PushViewController(new ConfigurationTableViewController(new TopComittersTableSource(), name), true);
				break;
			case "Build Status":
				controller.PushViewController(new ConfigurationTableViewController(new BuildStatusTableSource(), name), true);
				break;
			case "Latest Commits":
				controller.PushViewController(new ConfigurationTableViewController(new LatestCommitsTableSource(), name), true);
				break;
			case "Working days left":
				controller.PushViewController(new ConfigurationTableViewController(new WorkingDaysLeftTableSource(), name), true);
				break;
			}
        }
    }
}

