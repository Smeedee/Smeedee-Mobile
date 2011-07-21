using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class LatestCommitsConfigTableViewController : UITableViewController
	{
		public LatestCommitsConfigTableViewController() : base ("LatestCommitsConfigTableViewController", null)
		{
			Title = "Latest commits";
			TableView.Source = new LatestCommitsConfigTableViewSource();
		}
	}
	
	public class LatestCommitsConfigTableViewSource : WidgetConfigTableViewSource
	{
		public LatestCommitsConfigTableViewSource() 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(LatestCommitsConfigTableViewController)).First())
		{
			
		}
	}
}

