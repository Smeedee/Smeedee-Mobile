using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee;

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
            ConfigureDependencies();
            RegisterAllSupportedWidgets();
            AddMainTabBarToMenu();
        }
        
        private void ConfigureDependencies()
        {
            app.ServiceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
            app.ServiceLocator.Bind<IModelService<BuildStatusBoard>>(new FakeBuildStatusService());
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
