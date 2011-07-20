using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Preferences;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;
using Smeedee.Android.Screens;
using Smeedee.Android.Widgets;
using Smeedee.Android.Widgets.Settings;
using Smeedee.Model;
using Exception = System.Exception;

namespace Smeedee.Android
{
    [Activity(
        Label = "Smeedee Mobile",
        Theme = "@android:style/Theme.NoTitleBar")]
    public class WidgetContainer : Activity
    {
        private readonly SmeedeeApp app = SmeedeeApp.Instance;
        private RealViewSwitcher flipper;
        private IEnumerable<IWidget> widgets;

        private ISharedPreferencesOnSharedPreferenceChangeListener preferenceChangeListener;
        private ISharedPreferences prefs;
        private bool hasSettingsChanged;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            flipper = FindViewById<RealViewSwitcher>(Resource.Id.Flipper);
            flipper.ScreenChanged += HandleScreenChanged;

            AddWidgetsToFlipper();

            foreach (var widget in widgets)
            {
                widget.DescriptionChanged += WidgetContainer_DescriptionChanged;
            }

            Log.Debug("SMEEDEE", "In WidgetContainer");
            Log.Debug("SMEEDEE", "URL: " + new Login().Url);
            Log.Debug("SMEEDEE", "Key: " + new Login().Key);
            Log.Debug("SMEEDEE", "Valid? " + new Login().IsValid());

            preferenceChangeListener = new SharedPreferencesChangeListener(() =>
                                                                               {
                                                                                   hasSettingsChanged = true;
                                                                               });
            prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            prefs.RegisterOnSharedPreferenceChangeListener(preferenceChangeListener);

            var previousSlide = LastNonConfigurationInstance;
            if (previousSlide != null)
            {
                flipper.CurrentScreen = ((Integer)previousSlide).IntValue();
            }
        }

        void HandleScreenChanged(object sender, EventArgs e)
        {
            SetCorrectTopBannerWidgetTitle();
            SetCorrectTopBannerWidgetDescription();
        }

        void WidgetContainer_DescriptionChanged(object sender, EventArgs e)
        {
            if (sender != flipper.CurrentView) return;
            SetCorrectTopBannerWidgetDescription();
            SetCorrectTopBannerWidgetTitle();
        }

        private void AddWidgetsToFlipper()
        {
            widgets = GetWidgets();
            foreach (var widget in widgets)
            {
                flipper.AddView(widget as View);
            }
        }

        private IEnumerable<IWidget> GetWidgets()
        {
            app.RegisterAvailableWidgets();

            var availableWidgets = SmeedeeApp.Instance.AvailableWidgets;
            var instances = new List<IWidget>();
            foreach (var widget in availableWidgets)
            {
                try
                {
                    Log.Debug("Smeedee", "Instantiating widget of type: " + widget.Type.Name);
                    instances.Add(Activator.CreateInstance(widget.Type, this) as IWidget);
                }
                catch (Exception e)
                {
                    Log.Debug("Smeedee", Throwable.FromException(e), "Exception thrown when instatiating widget");
                }
            }
            return instances;
        }

        private void SetCorrectTopBannerWidgetTitle()
        {
            var widgetTitle = FindViewById<TextView>(Resource.Id.WidgetNameInTopBanner);
            widgetTitle.Text = GetWidgetNameOfCurrentlyDisplayedWidget();
        }

        private void SetCorrectTopBannerWidgetDescription()
        {
            var widgetDescriptionDynamic = FindViewById<TextView>(Resource.Id.WidgetDynamicDescriptionInTopBanner);
            var currentWidget = flipper.CurrentView as IWidget;

            widgetDescriptionDynamic.Text = (currentWidget != null) ? currentWidget.GetDynamicDescription() : "No widget";
        }

        private string GetWidgetNameOfCurrentlyDisplayedWidget()
        {
            return (from widget in SmeedeeApp.Instance.AvailableWidgets
                    where widget.Type == flipper.CurrentView.GetType()
                    select widget.Name).Single();
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.BtnRefreshCurrentWidget:
                    var currentWidget = flipper.CurrentView as IWidget;
                    if (currentWidget != null)
                    {
                        var dialog = ProgressDialog.Show(this, "Refreshing", "Updating data for current widget", true);
                        var handler = new ProgressHandler(dialog);
                        ThreadPool.QueueUserWorkItem(arg =>
                        {
                            currentWidget.Refresh();
                            handler.SendEmptyMessage(0);
                        });
                    }

                    return true;

                case Resource.Id.BtnWidgetSettings:

                    string widgetName = GetWidgetNameOfCurrentlyDisplayedWidget();

                    if (widgetName == "Build Status")
                        StartActivity(new Intent(this, typeof(BuildStatusSettings)));

                    if (widgetName == "Top Committers")
                        StartActivity(new Intent(this, typeof(TopCommittersSettings)));

                    if (widgetName == LatestCommitsWidget.Name)
                        StartActivity(new Intent(this, typeof(LatestCommitsSettings)));
                    return true;

                case Resource.Id.BtnGlobalSettings:

                    var globalSettings = new Intent(this, typeof(GlobalSettings));
                    StartActivity(globalSettings);
                    return true;

                case Resource.Id.BtnAbout:

                    var about = new Intent(this, typeof(About));
                    StartActivity(about);
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug("TT", "[ REFRESHING WIDGETS ]");

            if (hasSettingsChanged)
            {
                CheckForEnabledAndDisabledWidgets();
                SetCorrectTopBannerWidgetTitle();
                SetCorrectTopBannerWidgetDescription();
                
                //if (CheckIfWidgetSlideShowIsEnabled())
                //    StartWidgetSlideShow();
                //else
                //    StopWidgetSlideShow();

                Log.Debug("TT", "Just refreshed widget list after having changed settings.");
                hasSettingsChanged = false;
            }

            foreach (var widget in widgets)
            {
                widget.Refresh();
            }
        }

        //private bool CheckIfWidgetSlideShowIsEnabled()
        //{
        //    return prefs.GetBoolean("slideShowEnabled", false);
        //}

        //private void StartWidgetSlideShow()
        //{
        //    if (!flipper.IsFlipping)
        //    {
        //        //var flipInterval = int.Parse(prefs.GetString("slideShowInterval", "20000"));
        //        flipper.SetFlipInterval(2000);
        //        flipper.StartFlipping();
        //    }
        //}
        //private void StopWidgetSlideShow()
        //{
        //    if (flipper.IsFlipping)
        //        flipper.StopFlipping();
        //}

        private void CheckForEnabledAndDisabledWidgets()
        {
            var widgetModels = SmeedeeApp.Instance.AvailableWidgets;

            var newWidgets = new List<IWidget>();
            foreach (var widgetModel in widgetModels.Where(WidgetIsEnabled))
            {
                WidgetModel model = widgetModel;
                newWidgets.AddRange(widgets.Where(widget => widget.GetType() == model.Type));
            }

            flipper.RemoveAllViews();

            foreach (var newWidget in newWidgets)
            {
                flipper.AddView((View)newWidget);
            }
            flipper.CurrentScreen = 0;
        }

        private bool WidgetIsEnabled(WidgetModel widget)
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            return prefs.GetBoolean(widget.Name, true);
        }

        public override Java.Lang.Object OnRetainNonConfigurationInstance()
        {
            return flipper.CurrentScreen;
        }
    }
    
    public class SharedPreferencesChangeListener : ISharedPreferencesOnSharedPreferenceChangeListener
    {
        private readonly Action callbackOnPreferencesChanged;
        public SharedPreferencesChangeListener(Action callback)
        {
            callbackOnPreferencesChanged = callback;
        }
        public IntPtr Handle
        {
            get { throw new NotImplementedException(); }
        }

        public void OnSharedPreferenceChanged(ISharedPreferences sharedPreferences, string key)
        {
            callbackOnPreferencesChanged();
        }
    }

    class ProgressHandler : Handler
    {
        private readonly ProgressDialog dialog;
        public ProgressHandler(ProgressDialog dialog)
        {
            this.dialog = dialog;
        }
        public override void HandleMessage(Message msg)
        {
            base.HandleMessage(msg);
            dialog.Dismiss();
        }
    }
}
