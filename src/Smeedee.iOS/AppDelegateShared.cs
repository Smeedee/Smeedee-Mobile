using System;
using System.Drawing;
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
		private UIViewController loginHeaderController;
		private UIViewController loginController;
        
        public AppDelegateShared(UIWindow window, UITabBarController tabBarController, 
			UIViewController loginHeaderController, UIViewController loginController)
        {
            this.window = window;
            this.tabBarController = tabBarController;
			this.loginHeaderController = loginHeaderController;
			this.loginController = loginController;
        }
        
        public void FinishedLaunching()
        {
			loginHeaderController.View.Frame = new RectangleF(0, 0, 320, 150);
			loginController.View.Frame = new RectangleF(0, 150, 320, 460);
			
			window.AddSubviews(new [] {loginHeaderController.View, loginController.View});
			
			(loginController as ServerConfigTableViewController).LoginAction = delegate(string str) {
				if (str == Login.ValidationSuccess) {
	            	RegisterAllSupportedWidgets();
					window.InvokeOnMainThread( () => {
						AddMainTabBarToMenu();
					});
				}
			};
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
