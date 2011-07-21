using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	// TODO: Rename to TopCommittersConfigTableViewController
	public partial class TopCommittersWidgetTableViewController : UITableViewController
	{
		public TopCommittersWidgetTableViewController() : base("TopCommittersWidgetTableViewController", null)
		{
			this.Title = "Top Committers";
			this.TableView.Source = new TopCommittersWidgetTableSource(this);
		}
	}
	
	public class TopCommittersWidgetTableSource : WidgetConfigTableSource
	{
		private TopCommittersWidgetTableViewController controller;
		
		public TopCommittersWidgetTableSource(TopCommittersWidgetTableViewController controller) 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(TopCommittersWidgetTableViewController)).First())
		{
			this.controller = controller;
		}
		
		// TODO: The rest of the input controllers
		
	}
}

