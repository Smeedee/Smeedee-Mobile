using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iPhone
{
	public class Application
	{
		static void Main(string[] args)
		{
			UIApplication.Main(args);
		}
	}

	// The name AppDelegate is referenced in the MainWindow.xib file.
	public partial class AppDelegate : UIApplicationDelegate
	{
		private SmeedeeApp app = SmeedeeApp.Instance;
		
		// This method is invoked when the application has loaded its UI and its ready to run
		public override bool FinishedLaunching(UIApplication uiApp, NSDictionary options)
		{
			app.RegisterAvailableWidgets(new [] {
				typeof(SmeedeeWelcomeScreen),
				typeof(TopCommitersScreen)
			});
			
			window.AddSubview(tabBarController.View);

			window.MakeKeyAndVisible();
			return true;
		}
	}
}
