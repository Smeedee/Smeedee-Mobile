using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Services.Fakes;
using Smeedee.iOS.Lib;
using Smeedee;

namespace Smeedee.iOS
{
    public class Application
    {
		private const bool USE_FAKES = false;
		
        static void Main (string[] args)
        {
			ConfigureDependencies();
			RegisterAvailableWidgets();
			EnableAllWidgetsByDefault();
            UIApplication.Main (args);
        }
		
        private static void RegisterAvailableWidgets()
        {
            SmeedeeApp.Instance.RegisterAvailableWidgets();
        }
		
		private static void EnableAllWidgetsByDefault()
		{
			foreach (var widgetModel in SmeedeeApp.Instance.AvailableWidgets)
			{
				widgetModel.Enabled = true;
			}
		}
		
		private static void ConfigureDependencies() 
		{
			var serviceLocator = SmeedeeApp.Instance.ServiceLocator;
			
			serviceLocator.Bind<ILog>(new IphoneLogger());
			serviceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
			serviceLocator.Bind<IPersistenceService>(new IphoneKVPersister());
			
			serviceLocator.Bind<IFetchHttp>(new HttpFetcher());
			serviceLocator.Bind<IImageService>(new MemoryCachedImageService(new ImageService()));
			
			if (USE_FAKES)
			{
				serviceLocator.Bind<IValidationService>(new FakeValidationService());
				
				serviceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService());
				serviceLocator.Bind<ITopCommittersService>(new FakeTopCommittersService());
				serviceLocator.Bind<ILatestCommitsService>(new FakeLatestCommitsService());
				serviceLocator.Bind<IWorkingDaysLeftService>(new FakeWorkingDaysLeftService());
			}
			else
			{
				serviceLocator.Bind<IValidationService>(new ValidationService());
				
				serviceLocator.Bind<IBuildStatusService>(new BuildStatusService());
				serviceLocator.Bind<ITopCommittersService>(new TopCommittersService());
				serviceLocator.Bind<ILatestCommitsService>(new LatestCommitsService());
				serviceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftService());
			}
		}
    }
	
	internal static class Platform
	{
		public static Device Name = Device.Iphone;
		
		public static UIInterfaceOrientation Orientation = UIInterfaceOrientation.Portrait;
		
		public static int ScreenWidth 
		{
			get 
			{ 
				if (Name == Device.Iphone) 
					return IsPortrait() ? 320 : 480;
				return IsPortrait() ? 768 : 1024;
			}
		}
		public static int ScreenHeight
		{
			get 
			{
				if (Name == Device.Iphone)
					return IsPortrait() ? 480 : 320;
				return IsPortrait() ? 1024 : 768;
			}
		}
		
		private static bool IsPortrait()
		{
			return Orientation == UIInterfaceOrientation.Portrait || Orientation == UIInterfaceOrientation.PortraitUpsideDown;
		}
	}
	
	internal enum Device
	{
		Iphone, Ipad
	}
}
