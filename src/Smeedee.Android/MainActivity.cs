using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Smeedee.Android.Screens;
using Smeedee.Android.Services;
using Smeedee.Android.Widgets;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Utilities;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee Mobile", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/icon_smeedee")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ConfigureDependencies();
            
            var activity = DetermineNextActivity();
            StartActivity(activity);
        }

        private IPersistenceService persistor;
        [Serializable]
        internal class SerializableSimpleDataStructure
        {
            public double var1;
            public bool var2;
            public string var3;

            public override bool Equals(object obj)
            {
                var other = (SerializableSimpleDataStructure)obj;
                return (var1 == other.var1 && var2 == other.var2 && var3 == other.var3);
            }
        }
       
        private bool DictCompare(Dictionary<String, String> dict1, Dictionary<String, String> dict2)
        {
            if (dict1.Keys.Count != dict2.Keys.Count)
            {
                return false;
            }
            try
            {
                return dict1.Keys.All(key => dict1[key].Equals(dict2[key]));
            }
            catch
            {
                return false; 
            }
        }

        private void ConfigureDependencies()
        {
            var serviceLocator = SmeedeeApp.Instance.ServiceLocator;

            // Fill in global bindings here:
            serviceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
            
            serviceLocator.Bind<IModelService<BuildStatus>>(new FakeBuildStatusService());
            serviceLocator.Bind<ISmeedeeService>(new SmeedeeFakeService());
            serviceLocator.Bind<IModelService<WorkingDaysLeft>>(new WorkingDaysLeftFakeService());
            serviceLocator.Bind<IModelService<TopCommitters>>(new TopCommittersFakeService());
            serviceLocator.Bind<ILoginValidationService>(new FakeLoginValidationService());
            serviceLocator.Bind<ISmeedeeService>(new SmeedeeHttpService());
            serviceLocator.Bind<IChangesetService>(new FakeChangesetService());

            serviceLocator.Bind<IMobileKVPersister>(new AndroidKVPersister(this));
            serviceLocator.Bind<IPersistenceService>(new PersistenceService(
                                                        serviceLocator.Get<IMobileKVPersister>()
                                                    ));
        }

        private Intent DetermineNextActivity()
        {
            Type nextActivity = UserHasAValidUrlAndKey()
                                    ? typeof (WidgetContainer)
                                    : typeof (LoginScreen);
            return new Intent(this, nextActivity);
        }

        private bool UserHasAValidUrlAndKey()
        {
            var key = SmeedeeApp.Instance.GetStoredLoginKey();
            var url = SmeedeeApp.Instance.GetStoredLoginUrl();
            if (url == null || key == null) return false;

            var validator = SmeedeeApp.Instance.ServiceLocator.Get<ILoginValidationService>();
            return validator.IsValid(url, key);
        }
    }
}
