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
            UIApplication.Main (args);
        }
		
		private static void ConfigureDependencies() 
		{
			var serviceLocator = SmeedeeApp.Instance.ServiceLocator;
			serviceLocator.Bind<IModelService<LatestChangeset>>(new FakeLatestChangesetService());
			serviceLocator.Bind<IMobileKVPersister>(new IphoneKVPersister());
		}
    }
}
