using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
    public class AppDelegateShared
    {
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
				Console.WriteLine("Server is configured, attempting to connect ...");
				LoadingIndicator.Instance.StartLoading();
				ShowSplashScreen();
				login.StoreAndValidate(login.Url, login.Key, ServerCallback);
			}
        }
		
		private bool ServerIsConfigured()
		{
			return !string.IsNullOrEmpty(login.Url) && !string.IsNullOrEmpty(login.Key);
		}
		
		private void ServerCallback(string response)
		{
			LoadingIndicator.Instance.StopLoading();
			if (response == Login.ValidationSuccess) {
				Console.WriteLine("Login succeded, showing widgets");
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
			
			var label = new UILabel();
			label.Text = "Connecting ...";
			label.TextColor = StyleExtensions.darkGrayText;
			label.BackgroundColor = UIColor.Black;
			label.Frame = new RectangleF(120, 280, 200, 30);
			
			window.AddSubview(splash);
			window.AddSubview(label);
		}
		
		private void ShowServerConfiguration()
		{
			if (!previousWasServerConfig) {
				previousWasServerConfig = true;
				loginHeaderController.View.Frame = new RectangleF(0, 0, 320, 150);
				loginController.View.Frame = new RectangleF(0, 150, 320, 460);
				
				(loginController as ServerConfigTableViewController).LoginAction = ServerCallback;
				
				window.AddSubviews(new [] {loginHeaderController.View, loginController.View});
			}
		}
		
		private void ShowWidgets()
		{
            window.AddSubview(tabBarController.View);
		}
    }
}
