using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Services.Fakes;
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
            UIApplication.Main (args);
        }
		
		private static void ConfigureDependencies() 
		{
			var serviceLocator = SmeedeeApp.Instance.ServiceLocator;
			
			serviceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
			serviceLocator.Bind<IPersistenceService>(new IphoneKVPersister());
			
			serviceLocator.Bind<IFetchHttp>(new HttpFetcher());
			serviceLocator.Bind<IValidationService>(new ValidationService());
			serviceLocator.Bind<IImageService>(new ImageService(serviceLocator.Get<IBackgroundWorker>()));
			
			if (USE_FAKES)
			{
				serviceLocator.Bind<IValidationService>(new FakeValidationService());
				
				serviceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService(serviceLocator.Get<IBackgroundWorker>()));
				serviceLocator.Bind<ITopCommittersService>(new TopCommittersFakeService());
				serviceLocator.Bind<ILatestCommitsService>(new FakeLatestCommitsService());
				serviceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftFakeService());
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
		
        private static void RegisterAvailableWidgets()
        {
            SmeedeeApp.Instance.RegisterAvailableWidgets();
        }
    }
}
