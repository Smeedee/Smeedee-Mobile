using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Util;
using Smeedee.Android.Screens;
using Smeedee.Android.Services;
using Smeedee.Model;

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
            var serviceLocator = ((SmeedeeApplication)Application).App.ServiceLocator;

            // Fill in global bindings here:
            serviceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());

            serviceLocator.Bind<IModelService<BuildStatus>>(new FakeBuildStatusService());
            serviceLocator.Bind<IModelService<LatestChangeset>>(new FakeLatestChangesetService());
            serviceLocator.Bind<IModelService<WorkingDaysLeft>>(new WorkingDaysLeftFakeService());
            serviceLocator.Bind<IModelService<TopCommitters>>(new TopCommittersFakeService());
            serviceLocator.Bind<ILoginValidationService>(new FakeLoginValidationService());

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

            var pref = PreferenceManager.GetDefaultSharedPreferences(this);

            var url = pref.GetString("serverUrl", "url");
            var key = pref.GetString("userPassword", "pass");

            if (url == null || key == null) return false;

            var validator = SmeedeeApp.Instance.ServiceLocator.Get<ILoginValidationService>();
            return validator.IsValid(url, key);
        }
    }
}
