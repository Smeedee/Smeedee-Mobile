using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
    public partial class AppDelegateIPhone : UIApplicationDelegate
    {
        // This method is invoked when the application has loaded its UI and its ready to run
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
			Platform.Name = Device.Iphone;
            var sharedApp = new AppDelegateShared(window, tabBar, login);
            sharedApp.FinishedLaunching();

            window.MakeKeyAndVisible();
            return true;
        }
    }
}
