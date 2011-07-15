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
					
					var textField = new UITextField(cell.Frame);
					textField.Text = persister.Get<string>("Server.Url", null);
					
					textField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					textField.LeftView = new UIView(new RectangleF(0, 0, 80, 31)); 
					textField.LeftViewMode = UITextFieldViewMode.Always;
					textField.ReturnKeyType = UIReturnKeyType.Done;
					textField.ShouldReturn = delegate { 
						persister.Save("Server.Url", textField.Text);
						textField.ResignFirstResponder();
						return true; 
					};
					
					cell.AddSubview(textField);
					return cell;
				}
				else {
					var cell = new UITableViewCell();
					cell.TextLabel.Text = "Key";
					
					var keyField = new UITextField(cell.Frame);
					keyField.Text = persister.Get<string>("Server.Key", null);
					
					keyField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					keyField.LeftView = new UIView(new RectangleF(0,0, 80, 31)); 
					keyField.LeftViewMode = UITextFieldViewMode.Always;
					keyField.ReturnKeyType = UIReturnKeyType.Done;
					keyField.ShouldReturn = delegate { 
						persister.Save("Server.Key", keyField.Text);
						keyField.ResignFirstResponder();
						return true; 
					};
					
					cell.AddSubview(keyField);
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

