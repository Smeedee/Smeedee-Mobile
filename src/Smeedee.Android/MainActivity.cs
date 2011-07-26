using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Util;
using Smeedee.Android.Screens;
using Smeedee.Android.Services;
using Smeedee;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Services.Fakes;

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
            App.ServiceLocator.Bind<IPersistenceService>(new AndroidKVPersister(this));
            App.ServiceLocator.Bind<IFetchHttp>(new HttpFetcher());


            App.ServiceLocator.Bind<IBuildStatusService>(new BuildStatusService());
            App.ServiceLocator.Bind<ILatestCommitsService>(new LatestCommitsService());
            App.ServiceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftService());
            App.ServiceLocator.Bind<ITopCommittersService>(new TopCommittersService());
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
            
            var activity = DetermineNextActivity();
            StartActivity(activity);
            Finish();
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
