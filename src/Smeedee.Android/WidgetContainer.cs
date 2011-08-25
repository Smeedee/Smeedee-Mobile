using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;
using Smeedee.Android.Screens;
using Smeedee.Android.Widgets;
using Smeedee.Model;
using Smeedee.Services;
using Exception = System.Exception;
using Object = System.Object;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee Mobile", Theme = "@android:style/Theme.NoTitleBar")]
    public class WidgetContainer : Activity
    {
        private const string CURRENT_SCREEN_PERSISTENCE_KEY = "WidgetContainer.CurrentScreen";
        private readonly TimeSpan REFRESH_BUTTON_TO_BE_SHOWN_LIMIT_IN_MINUTES = new TimeSpan(0, 10, 0);
        private readonly SmeedeeApp app = SmeedeeApp.Instance;
        private IBackgroundWorker bgWorker;
        private ILog logger;
        private RealViewSwitcher flipper;
        private static IEnumerable<IWidget> _widgets;

        private Button _bottomRefreshButton;
        private Timer _timer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
            logger = SmeedeeApp.Instance.ServiceLocator.Get<ILog>();

            flipper = FindViewById<RealViewSwitcher>(Resource.Id.WidgetContainerFlipper);
            flipper.ScreenChanged += HandleScreenChanged;

            _bottomRefreshButton = FindViewById<Button>(Resource.Id.WidgetContainerBtnBottomRefresh);
            _bottomRefreshButton.Click += delegate { RefreshCurrentWidget(); };

            AddWidgetsToFlipper();

            foreach (var widget in _widgets)
            {
                widget.DescriptionChanged += WidgetDescriptionChanged;
            }
        }

        private void HandleScreenChanged(object sender, EventArgs e)
        {
            SetCorrectTopBannerWidgetTitle();
            SetCorrectTopBannerWidgetDescription();
            ShowRefreshButtonAtBottom(null);
        }

        private void SetCorrectTopBannerWidgetTitle()
        {
            var widgetTitle = FindViewById<TextView>(Resource.Id.WidgetContainerWidgetNameInTopBanner);
            widgetTitle.Text = GetWidgetNameOfCurrentlyDisplayedWidget();
        }

        private void SetCorrectTopBannerWidgetDescription()
        {
            var currentWidget = flipper.CurrentView as IWidget;

            var widgetDescription = FindViewById<TextView>(Resource.Id.WidgetContainerWidgetDynamicDescriptionInTopBanner);
            widgetDescription.Text = (currentWidget != null)
                                                ? currentWidget.GetDynamicDescription()
                                                : "No description";
        }

        private string GetWidgetNameOfCurrentlyDisplayedWidget()
        {
            return (from widget in SmeedeeApp.Instance.AvailableWidgets
                    where widget.Type == flipper.CurrentView.GetType()
                    select widget.Name).Single();
        }

        private void ShowRefreshButtonAtBottom(Object timer)
        {
            if (timer != null)
            {
                var t = (Timer) timer;
                t.Dispose();
            }
            var currentWidget = flipper.CurrentView as IWidget;
            if (currentWidget == null || currentWidget.GetType() == typeof (StartPageWidget) ||
                currentWidget.GetType() == typeof (WorkingDaysLeftWidget)) return;
            if ((DateTime.Now - currentWidget.LastRefreshTime()) <= REFRESH_BUTTON_TO_BE_SHOWN_LIMIT_IN_MINUTES) return;
            
            _bottomRefreshButton.Visibility = ViewStates.Invisible;
            _bottomRefreshButton.Text =
                (DateTime.Now - currentWidget.LastRefreshTime()).Minutes +
                " minutes since last refresh. Click to refresh";
            _bottomRefreshButton.Visibility = ViewStates.Visible;
        }

        private void WidgetDescriptionChanged(object sender, EventArgs e)
        {
            if (sender != flipper.CurrentView) return;
            SetCorrectTopBannerWidgetDescription();
            SetCorrectTopBannerWidgetTitle();
        }

        private void AddWidgetsToFlipper()
        {
            _widgets = GetWidgets();

            foreach (var widget in _widgets.Where(widget => widget.GetType() != typeof (StartPageWidget)))
            {
                flipper.AddView(widget as View);
            }
        }

        private IEnumerable<IWidget> GetWidgets()
        {
            SmeedeeApp.Instance.RegisterAvailableWidgets();

            var availableWidgets = SmeedeeApp.Instance.AvailableWidgets;
            var instances = new List<IWidget>();
            foreach (var widget in availableWidgets)
            {
                try
                {
                    logger.Log("SMEEDEE", "Instantiating widget of type: " + widget.Type.Name);
                    instances.Add(Activator.CreateInstance(widget.Type, this) as IWidget);
                }
                catch (Exception e)
                {
                    logger.Log("SMEEDEE", Throwable.FromException(e).ToString(), "Exception thrown when instatiating widget");
                }
            }
            return instances;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);

            var refreshMenuBtn = menu.FindItem(Resource.Id.MenuBtnRefreshCurrentWidget);
            if (flipper.CurrentView.GetType() == typeof(StartPageWidget))
                refreshMenuBtn.SetEnabled(false);

            var configMenuBtn = menu.FindItem(Resource.Id.MenuBtnWidgetSettings);
            var attribs =
                (WidgetAttribute) (flipper.CurrentView.GetType().GetCustomAttributes(typeof (WidgetAttribute), true)[0]);

            if (configMenuBtn != null)
                configMenuBtn.SetEnabled(attribs.SettingsType != null);

            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.MenuBtnRefreshCurrentWidget:
                    RefreshCurrentWidget();
                    return true;

                case Resource.Id.MenuBtnWidgetSettings:

                    var widgetName = GetWidgetNameOfCurrentlyDisplayedWidget();
                    var widgetModel = SmeedeeApp.Instance.AvailableWidgets.Single(wm => wm.Name == widgetName);

                    if (widgetModel.SettingsType != null)
                        StartActivity(new Intent(this, widgetModel.SettingsType));
                    return true;

                case Resource.Id.MenuBtnGlobalSettings:

                    var globalSettings = new Intent(this, typeof (GlobalSettings));
                    StartActivity(globalSettings);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        private void RefreshCurrentWidget()
        {
            var currentWidget = flipper.CurrentView as IWidget;
            if (currentWidget == null) return;

            logger.Log("SMEEDEE", "Refreshing widget");
            var dialog = ProgressDialog.Show(this, "", "Refreshing...", true);
            var handler = new ProgressHandler(dialog);
            bgWorker.Invoke(() =>
                                {
                                    currentWidget.Refresh();
                                    handler.SendEmptyMessage(0);
                                });
            SetCorrectTopBannerWidgetDescription();
            SetCorrectTopBannerWidgetTitle();
            HideTheBottomRefreshButton();
            StartRefreshTimer();
        }

        private void HideTheBottomRefreshButton()
        {
            _bottomRefreshButton.Visibility = ViewStates.Invisible;
        }

        protected override void OnResume()
        {
            base.OnResume();

            // This could be optimized. Now, all widgets gets refreshed for every little change in both gloabl settings
            // and each widget configuration
            
            CheckForEnabledAndDisabledWidgets();
            flipper.CurrentScreen = app.ServiceLocator.Get<IPersistenceService>().Get(CURRENT_SCREEN_PERSISTENCE_KEY, 0);

            logger.Log("SMEEDEE", "OnResume starting to refreshing widgets");
            RefreshAllCurrentlyEnabledWidgets(); // This taks some time in background threads

            SetCorrectTopBannerWidgetTitle();
            SetCorrectTopBannerWidgetDescription();
            HideTheBottomRefreshButton();
            StartRefreshTimer();
            HideTheStartUpRefreshWheel();
        }
        public override void OnConfigurationChanged(Configuration newConfig)
        {
        }
        private void HideTheStartUpRefreshWheel()
        {
            var startUpRefreshWheel = FindViewById<ProgressBar>(Resource.Id.WidgetContainerProgressBar);
            startUpRefreshWheel.Visibility = ViewStates.Invisible;
        }

        private void StartRefreshTimer()
        {
            _timer = new Timer(ShowRefreshButtonAtBottom);
            _timer.Change(REFRESH_BUTTON_TO_BE_SHOWN_LIMIT_IN_MINUTES.Milliseconds, 0);
        }

        private void RefreshAllCurrentlyEnabledWidgets()
        {
            for (var i = 0; i < flipper.ChildCount; i++)
            {
                var widget = flipper.GetChildAt(i) as IWidget;
                if (widget != null)
                {
                    widget.Refresh();
                }
            }
        }

        private void CheckForEnabledAndDisabledWidgets()
        {
            flipper.RemoveAllViews();

            var widgetModels = SmeedeeApp.Instance.EnabledWidgets;
            var enabledWidgets = new List<IWidget>();

            foreach (var widgetModel in widgetModels)
            {
                enabledWidgets.AddRange(_widgets.Where(widget => widget.GetType() == widgetModel.Type && widget.GetType() != typeof(StartPageWidget)));
            }

            if (enabledWidgets.Count() > 0)
            {
                foreach (var enabledWidget in enabledWidgets)
                {
                    flipper.AddView((View)enabledWidget);
                }
            }
            else
                flipper.AddView((View)_widgets.Single(w => w.GetType() == typeof(StartPageWidget)));
            flipper.CurrentScreen = 0;
        }

        // Called when another activity comes in foreground of this one
        protected override void OnPause()
        {
            base.OnPause();
            app.ServiceLocator.Get<IPersistenceService>().Save(CURRENT_SCREEN_PERSISTENCE_KEY, flipper.CurrentScreen);
        }
        // Called when the activity is no longer visible
        protected override void OnStop()
        {
            base.OnStop();
            app.ServiceLocator.Get<IPersistenceService>().Save(CURRENT_SCREEN_PERSISTENCE_KEY, flipper.CurrentScreen);
        }
    }

    public class ProgressHandler : Handler
    {
        private readonly ProgressDialog _dialog;
        
        public ProgressHandler(ProgressDialog dialog)
        {
            _dialog = dialog;
        }
        
        public override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);
            
            if (msg.What == 0) 
                _dialog.Dismiss();
        }
    }
}
