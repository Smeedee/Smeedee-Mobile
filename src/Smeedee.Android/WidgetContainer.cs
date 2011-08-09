using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Smeedee.Android.Screens;
using Smeedee.Android.Widgets;
using Smeedee.Model;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee Mobile", Theme = "@android:style/Theme.NoTitleBar")]
    public class WidgetContainer : Activity
    {
        private const string CURRENT_SCREEN_PERSISTENCE_KEY = "WidgetContainer.CurrentScreen";
        private readonly TimeSpan REFRESH_BUTTON_TO_BE_SHOWN_LIMIT_IN_MINUTES = new TimeSpan(0, 5, 0);
        private readonly SmeedeeApp app = SmeedeeApp.Instance;
        private RealViewSwitcher flipper;
        private static IEnumerable<IWidget> _widgets;

        private Button _bottomRefreshButton;
        private Timer _timer;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.Main);

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

        public static void SetWidgets(IEnumerable<IWidget> widgets)
        {
            _widgets = widgets;
        }

        void HandleScreenChanged(object sender, EventArgs e)
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
            var widgetDescriptionDynamic = FindViewById<TextView>(Resource.Id.WidgetContainerWidgetDynamicDescriptionInTopBanner);
            var currentWidget = flipper.CurrentView as IWidget;

            widgetDescriptionDynamic.Text = (currentWidget != null) ? currentWidget.GetDynamicDescription() : "No description";
        }

        private string GetWidgetNameOfCurrentlyDisplayedWidget()
        {
            return (from widget in SmeedeeApp.Instance.AvailableWidgets
                    where widget.Type == flipper.CurrentView.GetType()
                    select widget.Name).Single();
        }
        private void ShowRefreshButtonAtBottom(System.Object timer)
        {
            if (timer != null)
            {
                var t = (Timer) timer;
                t.Dispose();
            }
            var currentWidget = flipper.CurrentView as IWidget;
            if (currentWidget != null && currentWidget != typeof(StartPageWidget))
            {
                if ((DateTime.Now - currentWidget.LastRefreshTime()) > REFRESH_BUTTON_TO_BE_SHOWN_LIMIT_IN_MINUTES)
                {
                    _bottomRefreshButton.Visibility = ViewStates.Invisible;
                    _bottomRefreshButton.Text =
                        (DateTime.Now - currentWidget.LastRefreshTime()).Minutes + " minutes since last refresh. Click to refresh";
                    _bottomRefreshButton.Visibility = ViewStates.Visible;
                    
                }
            }
        }

        void WidgetDescriptionChanged(object sender, EventArgs e)
        {
            if (sender != flipper.CurrentView) return;
            SetCorrectTopBannerWidgetDescription();
            SetCorrectTopBannerWidgetTitle();
        }

        private void AddWidgetsToFlipper()
        {
            foreach (var widget in _widgets)
            {
                if (widget.GetType() == typeof(StartPageWidget)) continue;
                flipper.AddView(widget as View);
            }
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);

            var configMenuItem = menu.FindItem(Resource.Id.MenuBtnWidgetSettings);
            var attribs = (WidgetAttribute)(flipper.CurrentView.GetType().GetCustomAttributes(typeof(WidgetAttribute), true)[0]);

            if (configMenuItem != null)
                configMenuItem.SetEnabled(attribs.SettingsType != null);
            
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
                    {
                        StartActivity(new Intent(this, widgetModel.SettingsType));
                    }
                    return true;

                case Resource.Id.MenuBtnGlobalSettings:

                    var globalSettings = new Intent(this, typeof(GlobalSettings));
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

            var dialog = ProgressDialog.Show(this, "", "Refreshing widget", true);
            var handler = new ProgressHandler(dialog);
            var bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
            bgWorker.Invoke(() =>
                                {
                                    currentWidget.Refresh();
                                    handler.SendEmptyMessage(0);
                                });
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

            // This could be optimized. Now, all widgets gets refreshed for every little change in settings
            CheckForEnabledAndDisabledWidgets();
            flipper.CurrentScreen = app.ServiceLocator.Get<IPersistenceService>().Get(CURRENT_SCREEN_PERSISTENCE_KEY, 0);
            SetCorrectTopBannerWidgetTitle();
            SetCorrectTopBannerWidgetDescription();
            RefreshAllCurrentlyEnabledWidgets();
            HideTheBottomRefreshButton();
            StartRefreshTimer();
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

            var newWidgets = new List<IWidget>();
            foreach (var widgetModel in widgetModels)
            {
                newWidgets.AddRange(_widgets.Where(widget => widget.GetType() == widgetModel.Type && widget.GetType() != typeof(StartPageWidget)));
            }

            if (newWidgets.Count() > 0)
            {
                foreach (var newWidget in newWidgets)
                {
                    flipper.AddView((View)newWidget);
                }
            }
            else
            {   // If there are now widgets enabled; add the Start Page Widget
                flipper.AddView((View)_widgets.Single(w => w.GetType() == typeof(StartPageWidget)));
            }
            flipper.CurrentScreen = 0;
        }

        protected override void OnPause()
        {
            base.OnPause();
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
            _dialog.Dismiss();
        }
    }
}
