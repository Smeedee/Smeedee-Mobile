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
		
		private Login login;
		private bool previousWasServerConfig;
        
        public AppDelegateShared(UIWindow window, UITabBarController tabBarController, 
			UIViewController loginHeaderController, UIViewController loginController)
        {
            this.window = window;
            this.tabBarController = tabBarController;
			this.loginHeaderController = loginHeaderController;
			this.loginController = loginController;
			login = new Login();
			previousWasServerConfig = false;
        }
        
        public void FinishedLaunching()
        {
			if (!ServerIsConfigured())
			{
				Console.WriteLine("Server is not configured");
				
				ShowServerConfiguration();
			}
			else
			{
				ShowSplashScreen();
				
				Console.WriteLine("Server is configured, attempting to connect ...");
				
				login.StoreAndValidate(login.Url, login.Key, ServerCallback);
			}
        }
		
		private bool ServerIsConfigured()
		{
			return !string.IsNullOrEmpty(Login.LoginUrl) && !string.IsNullOrEmpty(Login.LoginKey);
		}
		
		private void ServerCallback(string response)
		{
			if (response == Login.ValidationSuccess) {
				Console.WriteLine("Login succeded, showing widgets");
            	RegisterAllSupportedWidgets(); // ?
				window.InvokeOnMainThread(ShowWidgets);
			}
			else
			{
				Console.WriteLine("Login failed, showing server configuration");
				window.InvokeOnMainThread(ShowServerConfiguration);
			}
		}
		
		private void ShowSplashScreen()
		{
			var image = UIImage.FromFile("images/logo.png");
			var splash = new UIImageView(image);
			const int imageSize = 61;
			splash.Frame = new RectangleF(320/2f-imageSize/2f, 460/2f-imageSize/2f, (float)imageSize, (float)imageSize);
			
			window.AddSubview(splash);
		}
		
		private void ShowServerConfiguration()
		{
			if (!previousWasServerConfig) {
				previousWasServerConfig = true;
				loginHeaderController.View.Frame = new RectangleF(0, 0, 320, 150);
				loginController.View.Frame = new RectangleF(0, 150, 320, 460);
				
				window.AddSubviews(new [] {loginHeaderController.View, loginController.View});
				
				(loginController as ServerConfigTableViewController).LoginAction = ServerCallback;
			}
		}
		
		private void ShowWidgets()
		{
            window.AddSubview(tabBarController.View);
		}
		
        private void RegisterAllSupportedWidgets()
        {
            app.RegisterAvailableWidgets();
        }
    }
}
