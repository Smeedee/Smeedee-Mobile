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
			serviceLocator.Bind<IPersistenceService>(new IphoneKVPersister());
			serviceLocator.Bind<IValidationService>(new FakeValidationService());
			serviceLocator.Bind<ITopCommittersService>(new TopCommittersFakeService());
			
			serviceLocator.Bind<IFetchHttp>(new HttpFetcher());
			serviceLocator.Bind<ILatestCommitsService>(new LatestCommitsService());
			
			serviceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService(serviceLocator.Get<IBackgroundWorker>()));
			serviceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftFakeService());
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
