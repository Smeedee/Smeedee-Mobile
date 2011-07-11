using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
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
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            var key = prefs.GetString("userPassword", "<unset>");
            var url = prefs.GetString("serverUrl", "<unset>");
            if (url == null || key == null) return false;

            var validator = SmeedeeApp.Instance.ServiceLocator.Get<ILoginValidationService>();
            return validator.IsValid(url, key);
        }
    }
}
