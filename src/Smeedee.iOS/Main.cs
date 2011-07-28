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
        static void Main (string[] args)
        {
			ConfigureDependencies();
			AssureSettingsExist();
            UIApplication.Main (args);
        }
		
		private static void ConfigureDependencies() 
		{
			var serviceLocator = SmeedeeApp.Instance.ServiceLocator;
			
			serviceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
			serviceLocator.Bind<IFetchHttp>(new HttpFetcher());
			serviceLocator.Bind<IPersistenceService>(new IphoneKVPersister());
			serviceLocator.Bind<IValidationService>(new ValidationService());
			serviceLocator.Bind<ITopCommittersService>(new TopCommittersService());
			
			serviceLocator.Bind<IImageService>(new ImageService(serviceLocator.Get<IBackgroundWorker>()));
			serviceLocator.Bind<ILatestCommitsService>(new LatestCommitsService());
			
			serviceLocator.Bind<IBuildStatusService>(new BuildStatusService());
			serviceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftService());
		}
		
		private static void AssureSettingsExist() 
		{	
			PutIfNull("Server.Url", "http://www.smeedee.com/app");
			PutIfNull("Server.Key", "password");
			
			// All widgets are enabled by default in WidgetsScreen
		}
		
		private static void PutIfNull(string key, string val) {
			var persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
			var currentValue = persistence.Get(key, "");
			if (currentValue == "") 
				persistence.Save(key, val);
		}
    }
	
}
