﻿using System;
using Android.App;
using Android.Content;
using Android.OS;
using Smeedee.Android.Lib;
using Smeedee.Android.Screens;
using Smeedee.Android.Services;
using Smeedee.Lib;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Services.Fakes;

namespace Smeedee.Android
{
    [Application]
    public class SmeedeeApplication : Application
    {
        public SmeedeeApp App { get; private set; }
        private const bool USE_FAKES = false;

        public SmeedeeApplication(IntPtr handle) : base(handle)
        {
            App = SmeedeeApp.Instance;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            // Fill in global bindings here:
            App.ServiceLocator.Bind<IFileIO>(new MonoFileIO());
            App.ServiceLocator.Bind<IFetchHttp>(new SimpleHttpFetcher());
            App.ServiceLocator.Bind<Directories>(new Directories() { CacheDir = this.CacheDir.AbsolutePath });
            App.ServiceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
            App.ServiceLocator.Bind<IPersistenceService>(new AndroidKVPersister(this));
            App.ServiceLocator.Bind<ILog>(new LogService());
            
            if (!USE_FAKES)
            {
                App.ServiceLocator.Bind<IValidationService>(new ValidationService());
                App.ServiceLocator.Bind<IImageService>(new MemoryCachedImageService(new ImageService()));

                App.ServiceLocator.Bind<IBuildStatusService>(new BuildStatusService());
                App.ServiceLocator.Bind<ILatestCommitsService>(new LatestCommitsService());
                App.ServiceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftService());
                App.ServiceLocator.Bind<ITopCommittersService>(new TopCommittersService());
            }
            
            else
            {
                App.ServiceLocator.Bind<IValidationService>(new FakeValidationService());
                App.ServiceLocator.Bind<IImageService>(new MemoryCachedImageService(new ImageService()));

                App.ServiceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService());
                App.ServiceLocator.Bind<ILatestCommitsService>(new FakeLatestCommitsService());
                App.ServiceLocator.Bind<IWorkingDaysLeftService>(new FakeWorkingDaysLeftService());
                App.ServiceLocator.Bind<ITopCommittersService>(new FakeTopCommittersService());
            }
        }
    }

    [Activity(Label = "Smeedee Mobile", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar", Icon = "@drawable/icon_smeedee")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.StartUpLoadingScreen);

            var login = new Login();
            if (login.Url == "" && login.Key == "")
            {
                login.Url = Login.DefaultSmeedeeUrl;
                login.Key = Login.DefaultSmeedeeKey;
            }

            login.IsValid(valid =>
            {
                var nextActivity = valid ? typeof(WidgetContainer) : typeof(LoginScreen);
                StartActivity(new Intent(this, nextActivity));
                Finish();
            });
        }
    }
}
