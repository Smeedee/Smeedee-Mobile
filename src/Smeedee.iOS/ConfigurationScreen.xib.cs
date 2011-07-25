using System;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
    public partial class ConfigurationScreen : UINavigationController
    {
        public ConfigurationScreen(IntPtr handle) : base(handle) { }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			PushViewController( new MainConfigTableViewController(), false);
        }
    }
}
