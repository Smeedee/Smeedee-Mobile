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

            App.ServiceLocator.Bind<IModelService<BuildStatus>>(new FakeBuildStatusService());
            App.ServiceLocator.Bind<IModelService<LatestChangeset>>(new FakeLatestChangesetService());
            App.ServiceLocator.Bind<IModelService<WorkingDaysLeft>>(new WorkingDaysLeftFakeService());
            App.ServiceLocator.Bind<IModelService<TopCommitters>>(new TopCommittersFakeService());

            App.ServiceLocator.Bind<IPersistenceService>(new AndroidKVPersister(this));
        }
    }

    [Activity(Label = "Smeedee Mobile", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/icon_smeedee")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Log.Debug("SMEEDEE", "In MainActivity");
            Log.Debug("SMEEDEE", "URL: " + new Login().Url);
            Log.Debug("SMEEDEE", "Key: " + new Login().Key);
            Log.Debug("SMEEDEE", "Valid? "+ new Login().IsValid());

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
            Type nextActivity = new Login().IsValid()
                                    ? typeof (WidgetContainer)
                                    : typeof (LoginScreen);
            return new Intent(this, nextActivity);
        }
    }
}
