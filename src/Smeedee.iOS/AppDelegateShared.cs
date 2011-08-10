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
		private ILog logger = SmeedeeApp.Instance.ServiceLocator.Get<ILog>();
		
        private UIWindow window;
        private UITabBarController tabBarController;
		private UIViewController loginHeaderController;
		private UIViewController loginController;
		
		private Login login;
		private bool previousWasServerConfig;
        
        public AppDelegateShared(UIWindow window, UITabBarController tabBarController, UIViewController loginController)
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
				logger.Log("Server is not configured");
				ShowServerConfiguration();
			}
			else
			{
				logger.Log("Server is configured, attempting to connect");
				LoadingIndicator.Instance.StartLoading();
				ShowSplashScreen();
				login.ValidateAndStore(login.Url, login.Key, (response) => {
					LoadingIndicator.Instance.StopLoading();
					ServerCallback(response);
				});
			}
        }
		
		private bool ServerIsConfigured()
		{
			return !string.IsNullOrEmpty(login.Url) && !string.IsNullOrEmpty(login.Key);
		}
		
		private void ServerCallback(string response)
		{
			if (response == Login.ValidationSuccess) {
				logger.Log("Login succeded, showing widgets");
				window.InvokeOnMainThread(ShowWidgets);
			}
			else
			{
				logger.Log("Login failed, showing server configuration");
				window.InvokeOnMainThread(ShowServerConfiguration);
			}
		}
		
		private void ShowSplashScreen()
		{
			var image = UIImage.FromFile("images/logo.png");
			var splash = new UIImageView(image);
			const int imageSize = 61;
			splash.Frame = new RectangleF(Platform.ScreenWidth/2f-imageSize/2f, Platform.ScreenHeight/2f-imageSize/2f, (float)imageSize, (float)imageSize);
			
			var label = new UILabel();
			label.Text = "Connecting ...";
			var labelWidth = 120;
			label.TextAlignment = UITextAlignment.Center;
			label.TextColor = StyleExtensions.darkGrayText;
			label.BackgroundColor = UIColor.Black;
			label.Frame = new RectangleF(Platform.ScreenWidth/2f-labelWidth/2f, splash.Frame.Y + splash.Frame.Height + 10, labelWidth, 30);
			
			window.AddSubview(splash);
			window.AddSubview(label);
		}
		
		private void ShowServerConfiguration()
		{
			if (!previousWasServerConfig) {
				previousWasServerConfig = true;
				loginController.View.Frame = new RectangleF(0, 0, Platform.ScreenWidth, Platform.ScreenHeight);
				
				(loginController as ServerConfigTableViewController).LoginAction = ServerCallback;
				
				window.AddSubview(loginController.View);
			}
		}
		
		private void ShowWidgets()
		{
            window.AddSubview(tabBarController.View);
		}
    }
}
