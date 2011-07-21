using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	public partial class TopCommittersConfigTableViewController : UITableViewController
	{
		public TopCommittersConfigTableViewController() : base("TopCommittersConfigTableViewController", null)
		{
			this.Title = "Top Committers";
			this.TableView.Source = new TopCommittersConfigTableSource();
		}
	}
	
	public class TopCommittersConfigTableSource : WidgetConfigTableSource
	{
		public TopCommittersConfigTableSource() 
			: base(SmeedeeApp.Instance.AvailableWidgets.Where(e => e.SettingsType == typeof(TopCommittersConfigTableViewController)).First())
		{
			
		}
		
		// TODO: The rest of the input controllers
	}
}
