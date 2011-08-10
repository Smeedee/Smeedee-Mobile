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
			this.NavigationBar.BarStyle = UIBarStyle.Black;
			PushViewController( new MainConfigTableViewController(), false);
        }
		
		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			Platform.Orientation = toInterfaceOrientation;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
    }
}
