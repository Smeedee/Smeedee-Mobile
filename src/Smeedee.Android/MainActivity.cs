using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Util;
using Smeedee.Android.Screens;
using Smeedee.Android.Services;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android
{
    [Application]
    public class SmeedeeApplication : Application
    {
        public SmeedeeApp App { get; private set; }

        public SmeedeeApplication(IntPtr handle) : base(handle)
        {
            App = SmeedeeApp.Instance;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Log.Debug("TT", "Application is being run");

            // Fill in global bindings here:
            App.ServiceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
            App.ServiceLocator.Bind<IMobileKVPersister>(new AndroidKVPersister(this));
            App.ServiceLocator.Bind<IPersistenceService>(new PersistenceService(
                                                        App.ServiceLocator.Get<IMobileKVPersister>()
                                                    ));


            App.ServiceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService(
                                                        App.ServiceLocator.Get<IBackgroundWorker>()));


            // TODO: Old Interfaces and whatnot
            App.ServiceLocator.Bind<IModelService<LatestChangeset>>(new FakeLatestChangesetService());
            App.ServiceLocator.Bind<IModelService<WorkingDaysLeft>>(new WorkingDaysLeftFakeService());
            App.ServiceLocator.Bind<IModelService<TopCommitters>>(new TopCommittersFakeService());
            App.ServiceLocator.Bind<ILoginValidationService>(new FakeLoginValidationService());

            
        }
    }

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
            // Now happens in SmeedeeApplication
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
            var persistance = ((SmeedeeApplication) Application).App.ServiceLocator.Get<IPersistenceService>();
            var url = persistance.Get("serverUrl", "");
            var key = persistance.Get("userPassword", "");

            if (url == null || key == null) return false;

            var validator = SmeedeeApp.Instance.ServiceLocator.Get<ILoginValidationService>();
            return validator.IsValid(url, key);
        }
    }
}
