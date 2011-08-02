using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Smeedee.Lib;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Services.Fakes;
using Smeedee.WP7.Services.Fakes;

namespace Smeedee.WP7
{
    public partial class App : Application
    {
        private static TopCommittersViewModel viewModel = null;

        /// <summary>
        /// A static ViewModel used by the views to bind against.
        /// </summary>
        /// <returns>The TopCommittersViewModel object.</returns>
        public static TopCommittersViewModel ViewModel
        {
            get
            {
                // Delay creation of the view model until necessary
                if (viewModel == null)
                    viewModel = new TopCommittersViewModel();

                return viewModel;
            }
        }

        /// <summary>
        /// Provides easy access to the root frame of the Phone Application.
        /// </summary>
        /// <returns>The root frame of the Phone Application.</returns>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Constructor for the Application object.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            UnhandledException += Application_UnhandledException;

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                //Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are being GPU accelerated with a colored overlay.
                //Application.Current.Host.Settings.EnableCacheVisualization = true;
            }

            // Standard Silverlight initialization
            InitializeComponent();

            // Phone-specific initialization
            InitializePhoneApplication();

            BindDependencies();
        }

        private bool USE_FAKES = true;
        private void BindDependencies()
        {
            
            var app = SmeedeeApp.Instance;
            //app.ServiceLocator.Bind<IFileIO>(new MonoFileIO());
            if (!USE_FAKES)
            {
                app.ServiceLocator.Bind<IBackgroundWorker>(new BackgroundWorker());
                //app.ServiceLocator.Bind<IPersistenceService>(new AndroidKVPersister(this));
                app.ServiceLocator.Bind<IFetchHttp>(new HttpFetcher());
                app.ServiceLocator.Bind<IValidationService>(new ValidationService());
                app.ServiceLocator.Bind<Directories>(new Directories() { CacheDir = "C:/" });
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