using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	// TODO: Rename to TopCommittersConfigTableViewController
	public partial class TopCommittersConfigTableViewController : UITableViewController
	{
		public TopCommittersConfigTableViewController() : base("TopCommittersWidgetTableViewController", null)
		{
			this.Title = "Top Committers";
			this.TableView.Source = new TopCommittersConfigTableSource(this);
		}
	}
	
	public class TopCommittersConfigTableSource : WidgetConfigTableSource
	{
		private TopCommittersConfigTableViewController controller;
		
		public TopCommittersConfigTableSource(TopCommittersConfigTableViewController controller) 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(TopCommittersConfigTableViewController)).First())
		{
			this.controller = controller;
		}
		
		// TODO: The rest of the input controllers
		
	}
}
