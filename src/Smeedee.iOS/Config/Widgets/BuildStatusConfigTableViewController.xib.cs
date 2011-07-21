using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class BuildStatusConfigTableViewController : UITableViewController
	{
		public BuildStatusConfigTableViewController() : base ("BuildStatusConfigTableViewController", null)
		{
			this.Title = "Build status";
			this.TableView.Source = new BuildStatusConfigTableSource();
		}
	}
	
	public class BuildStatusConfigTableSource : WidgetConfigTableSource
	{
		public BuildStatusConfigTableSource() 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(BuildStatusConfigTableViewController)).First())
		{
			
		}
	}
}

