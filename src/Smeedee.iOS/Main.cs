using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
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
			serviceLocator.Bind<IModelService<LatestChangeset>>(new FakeLatestChangesetService());
			serviceLocator.Bind<IMobileKVPersister>(new IphoneKVPersister());
			serviceLocator.Bind<IPersistenceService>(new PersistenceService(serviceLocator.Get<IMobileKVPersister>()));
		}
		
		private static void AssureSettingsExist() 
		{	
			PutIfNull("Server.Url", "http://www.smeedee.com/app");
			PutIfNull("Server.Key", "password");
			PutIfNull("EnabledWidgets", new Dictionary<string, bool>());
		}
		
		private static void PutIfNull<T>(string key, T val) where T : class {
			var persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
			var enabled = persistence.Get<T>(key, null);
			if (enabled == null) 
				persistence.Save(key, val);
		}
    }
	
}
