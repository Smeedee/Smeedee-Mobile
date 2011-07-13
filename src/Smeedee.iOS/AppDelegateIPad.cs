using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
    public partial class AppDelegateIPad : UIApplicationDelegate
    {
        // This method is invoked when the application has loaded its UI and its ready to run
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            var sharedApp = new AppDelegateShared(window, tabBar);
            sharedApp.FinishedLaunching();
   
            window.MakeKeyAndVisible();
            return true;
        }
    }
}