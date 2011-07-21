using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class WorkingDaysLeftConfigTableViewController : UITableViewController
	{
		public WorkingDaysLeftConfigTableViewController () : base ("WorkingDaysLeftConfigTableViewController", null)
		{
			Title = "Working days left";
			
			var widgetModel = SmeedeeApp.Instance.AvailableWidgets
								.Where(e => e.SettingsType == typeof(WorkingDaysLeftConfigTableViewController)).First();
			
			TableView.Source = new WidgetConfigTableViewSource(widgetModel);
		}
	}
}

