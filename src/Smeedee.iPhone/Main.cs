using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.Services;

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
            ConfigureDependencies();
            RegisterAllSupportedWidgets();
            AddMainTabBarToMenu();
            
            window.MakeKeyAndVisible();
            return true;
        }
        
        public override void HandleOpenURL(UIApplication application, NSUrl url)
        {
            Console.WriteLine("URL: " + url.AbsoluteUrl);
        }
        
        private void ConfigureDependencies()
        {
            app.ServiceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
			app.ServiceLocator.Bind<IModelService<BuildStatus>>(new FakeBuildStatusService());
			app.ServiceLocator.Bind<IModelService<WorkingDaysLeft>>(new WorkingDaysLeftFakeService());
			app.ServiceLocator.Bind<IModelService<TopCommitters>>(new TopCommittersFakeService());
			app.ServiceLocator.Bind<ILoginValidationService>(new FakeLoginValidationService());
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
