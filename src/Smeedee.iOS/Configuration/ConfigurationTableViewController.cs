using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
    public class ConfigurationTableViewController : UITableViewController
    {
		private UITableViewSource source;
		private string title;
		
        public ConfigurationTableViewController(UITableViewSource source, string title)
            : base(UITableViewStyle.Grouped)
        {
			this.source = source;
			this.title = title;
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = title;
            TableView.Source = source;
        }
    }
}

