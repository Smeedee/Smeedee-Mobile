using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Smeedee.Android.Screens;
using Smeedee.Android.Services;
using Smeedee.Android.Widgets;
using Smeedee.Model;
using Smeedee;

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
            var database = new AndroidKVPersister(this);

            var url = database.Get("serverUrl");
            var key = database.Get("userPassword");

            if (url == null || key == null) return false;

            var validator = SmeedeeApp.Instance.ServiceLocator.Get<ILoginValidationService>();
            return validator.IsValid(url, key);
        }
    }
}
