using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Smeedee.Android.Screens;
using Smeedee.Android.Services;
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
            Log.Debug("SMEEDEE", "Application is being run");

            // Fill in global bindings here:
            App.ServiceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
            App.ServiceLocator.Bind<IPersistenceService>(new AndroidKVPersister(this));
            App.ServiceLocator.Bind<IFetchHttp>(new HttpFetcher());
            App.ServiceLocator.Bind<IValidationService>(new ValidationService());
            App.ServiceLocator.Bind<Directories>(new Directories() { CacheDir = this.CacheDir.AbsolutePath });
            //App.ServiceLocator.Bind<IImageService>(new MemoryCachedImageService(new DiskCachedImageService(new ImageService())));
            App.ServiceLocator.Bind<IImageService>(new MemoryCachedImageService(new ImageService()));

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
            
            new Login().IsValid(valid =>
            {
                var nextActivity = valid ? typeof(WidgetContainer) : typeof(LoginScreen);
                StartActivity(new Intent(this, nextActivity));
                Finish();
            });
        }
    }
}
