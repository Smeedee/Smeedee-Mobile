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
					textField.Text = "http://www.smeedee.com/app";
					
					textField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					textField.LeftView = new UIView(new RectangleF(0,0, 80, 31)); 
					textField.LeftViewMode = UITextFieldViewMode.Always;
					
					cell.AddSubview(textField);
					return cell;
				}
				else {
					var cell = new UITableViewCell();
					cell.TextLabel.Text = "Key";
					
					var textField = new UITextField(cell.Frame);
					textField.Text = "smeedeepassword";
					
					textField.VerticalAlignment = UIControlContentVerticalAlignment.Center; 
					textField.LeftView = new UIView(new RectangleF(0,0, 80, 31)); 
					textField.LeftViewMode = UITextFieldViewMode.Always;
					
					cell.AddSubview(textField);
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
			
			if (indexPath.Section != 0) {
				controller.PushViewController(new ConfigurationTableViewController(new TopComittersTableSource(), "Top Committers"), true);
			}
        }
    }
}

