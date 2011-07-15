using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.iOS.Configuration;

using Smeedee.Model;

namespace Smeedee.iOS
{
    public partial class ConfigurationScreen : UINavigationController
    {
        #region Constructors

        public ConfigurationScreen(IntPtr handle) : base(handle)
        {
        }

        [Export ("initWithCoder:")]
        public ConfigurationScreen(NSCoder coder) : base(coder)
        {
        }

        public ConfigurationScreen() : base("ConfigurationScreen", null)
        {
        }

        #endregion
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			
            var configTableController = new ConfigurationTableViewController(new ConfigurationTableSource(this), "Settings");
            PushViewController(configTableController, false);
        }
    }
	
    
}
