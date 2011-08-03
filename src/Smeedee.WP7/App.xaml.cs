using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Smeedee.Lib;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Services.Fakes;
using Smeedee.WP7.Lib;
using Smeedee.WP7.Services.Fakes;
using Smeedee.WP7.ViewModels;

namespace Smeedee.WP7
{
    public partial class App : Application
    {
        private static MainViewModel viewModel = null;

        public static MainViewModel ViewModel
        {
            get
            {
                if (viewModel == null)
                    viewModel = new MainViewModel();

                return viewModel;
            }
        }

        public PhoneApplicationFrame RootFrame { get; private set; }

        public App()
        {
            UnhandledException += Application_UnhandledException;

            if (System.Diagnostics.Debugger.IsAttached)
            {
                Application.Current.Host.Settings.EnableFrameRateCounter = true;
            }

            InitializeComponent();

            InitializePhoneApplication();

            BindDependencies();
        }

        private bool USE_FAKES = false;
        private void BindDependencies()
        {
            
            var app = SmeedeeApp.Instance;
            //app.ServiceLocator.Bind<IFileIO>(new MonoFileIO());
            if (!USE_FAKES)
            {
                app.ServiceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
                //app.ServiceLocator.Bind<IPersistenceService>(new AndroidKVPersister(this));
                app.ServiceLocator.Bind<IPersistenceService>(new FakePersister()); //<-Still fake!
                app.ServiceLocator.Bind<IFetchHttp>(new HttpFetcher());
                app.ServiceLocator.Bind<IValidationService>(new ValidationService());
                app.ServiceLocator.Bind<Directories>(new Directories() { CacheDir = "" }); //We cache in the root of our IsolatedStorage, so we have an empty string here
                app.ServiceLocator.Bind<IFileIO>(new Wp7FileIO());
                app.ServiceLocator.Bind<IImageService>(new MemoryCachedImageService(new DiskCachedImageService(new ImageService())));

                app.ServiceLocator.Bind<IBuildStatusService>(new BuildStatusService());
                app.ServiceLocator.Bind<ILatestCommitsService>(new LatestCommitsService());
                app.ServiceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftService());
                app.ServiceLocator.Bind<ITopCommittersService>(new TopCommittersService());
            }

            else
            {
                app.ServiceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
                app.ServiceLocator.Bind<IPersistenceService>(new FakePersister());
                app.ServiceLocator.Bind<IFetchHttp>(new HttpFetcher());
                app.ServiceLocator.Bind<IValidationService>(new FakeValidationService());
                app.ServiceLocator.Bind<Directories>(new Directories() { CacheDir = "C:/" });
                app.ServiceLocator.Bind<IImageService>(new MemoryCachedImageService(new ImageService()));
                //app.ServiceLocator.Bind<IImageService>(new MemoryCachedImageService(new DiskCachedImageService(new ImageService())));

                app.ServiceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService());
                app.ServiceLocator.Bind<ILatestCommitsService>(new FakeLatestCommitsService());
                app.ServiceLocator.Bind<IWorkingDaysLeftService>(new FakeWorkingDaysLeftService());
                app.ServiceLocator.Bind<ITopCommittersService>(new FakeTopCommittersService());
            }
        }

        // Code to execute when the application is launching (eg, from Start)
        // This code will not execute when the application is reactivated
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
        }

        // Code to execute when the application is activated (brought to foreground)
        // This code will not execute when the application is first launched
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
            // Ensure that application state is restored appropriately
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        // Code to execute when the application is deactivated (sent to background)
        // This code will not execute when the application is closing
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
            // Ensure that required application state is persisted here.
        }

        // Code to execute if a navigation fails
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        // Code to execute on Unhandled Exceptions
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        #region Phone application initialization

        // Avoid double-initialization
        private bool phoneApplicationInitialized = false;

        // Do not add any additional code to this method
        private void InitializePhoneApplication()
        {
            if (phoneApplicationInitialized)
                return;

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            RootFrame = new PhoneApplicationFrame();
            RootFrame.Navigated += CompleteInitializePhoneApplication;

            // Handle navigation failures
            RootFrame.NavigationFailed += RootFrame_NavigationFailed;

            // Ensure we don't initialize again
            phoneApplicationInitialized = true;
        }

        // Do not add any additional code to this method
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (RootVisual != RootFrame)
                RootVisual = RootFrame;

            // Remove this handler since it is no longer needed
            RootFrame.Navigated -= CompleteInitializePhoneApplication;
        }

        #endregion
    }
}