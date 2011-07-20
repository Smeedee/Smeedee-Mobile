using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.iOS
{
    public class AppDelegateShared
    {
        private SmeedeeApp app = SmeedeeApp.Instance;
        private UIWindow window;
        private UITabBarController tabBarController;
        
        public AppDelegateShared(UIWindow window, UITabBarController tabBarController)
        {
            this.window = window;
            this.tabBarController = tabBarController;
        }
        
        public void FinishedLaunching()
        {
            RegisterAllSupportedWidgets();
            AddMainTabBarToMenu();
        }
        
        private void RegisterAllSupportedWidgets()
        {
            app.RegisterAvailableWidgets();
        }
        
        private void AddMainTabBarToMenu()
        {
            window.AddSubview(tabBarController.View);
        }
    }
}
