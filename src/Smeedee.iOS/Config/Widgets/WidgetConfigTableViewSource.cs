using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
	public class WidgetConfigTableViewSource : UITableViewSource
	{
		// Needs to be declared here to avoid GC issues
		// See http://stackoverflow.com/questions/6156165/why-does-my-uiswitch-crash-when-it-is-a-tableview-cell-accessoryview
		private DarkSwitch darkSwitch;
		
		protected WidgetModel widgetModel;
		
		public WidgetConfigTableViewSource(WidgetModel model)
		{
			widgetModel = model;
		}
		
		public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
		{
			darkSwitch = new DarkSwitch(widgetModel.Enabled);
			darkSwitch.ValueChanged += delegate {
				widgetModel.Enabled = darkSwitch.On;
			};
			
			var cell = new UITableViewCell(UITableViewCellStyle.Default, "EnableWidgetUISwitch") {
				AccessoryView = darkSwitch, 
			};
			cell.TextLabel.Text = "Enabled";
			cell.StyleAsSettingsTableCell();
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			
			return cell;
		}
		
        public override int NumberOfSections(UITableView tableView) { return 1; }
        public override int RowsInSection(UITableView tableview, int section) { return 1; }
	}
	
	internal class DarkSwitch : UISegmentedControl
	{
		public DarkSwitch(bool on)
		{
			InsertSegment("ON", 0, false);
			InsertSegment("OFF", 1, false);
			Frame = new System.Drawing.RectangleF(0, 0, 100, 30);
			
			ControlStyle = UISegmentedControlStyle.Bar;
			BackgroundColor = StyleExtensions.transparent;
			TintColor = UIColor.FromRGB(100, 100, 100);
			
			SelectedSegment = (on) ? 0 : 1;
		}
		
		public bool On 
		{
			get { return SelectedSegment == 0; }
		}
	}
}

