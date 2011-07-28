﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Preferences;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using Java.Lang;
using Smeedee.Android.Screens;
using Smeedee.Model;
using Exception = System.Exception;

namespace Smeedee.Android
{
    [Activity(
        Label = "Smeedee Mobile",
        Theme = "@android:style/Theme.NoTitleBar")]
    public class WidgetContainer : Activity
    {
        private const string CURRENT_SCREEN_PERSISTENCE_KEY = "WidgetContainer.CurrentScreen";
        //TODO: Change the variable below to 10 minutes. Use: new DateTime(0, 0, 0, 0, 10, ).Minute
        private readonly TimeSpan REFRESH_TEXT_TO_BE_SHOWN_LIMIT = new TimeSpan(0, 0, 5);
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
                widget.DescriptionChanged += WidgetDescriptionChanged;
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
        }

        void HandleScreenChanged(object sender, EventArgs e)
        {
            SetCorrectTopBannerWidgetTitle();
            SetCorrectTopBannerWidgetDescription();

            ShowRefreshTextAtBottomOrNot();
        }

        private void ShowRefreshTextAtBottomOrNot()
        {
            var currentWidget = flipper.CurrentView as IWidget;
            if (currentWidget != null)
            {
                if ((DateTime.Now - currentWidget.LastRefreshTime()) > REFRESH_TEXT_TO_BE_SHOWN_LIMIT)
                {
                    var refreshText = FindViewById<TextView>(Resource.Id.RefreshText);
                    refreshText.Text =
                        (DateTime.Now - currentWidget.LastRefreshTime()).PrettyPrint() + " since last refresh";
                    refreshText.Visibility = ViewStates.Visible;
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
                    Log.Debug("SMEEDEE", "Instantiating widget of type: " + widget.Type.Name);
                    instances.Add(Activator.CreateInstance(widget.Type, this) as IWidget);
                }
                catch (Exception e)
                {
                    Log.Debug("SMEEDEE", Throwable.FromException(e), "Exception thrown when instatiating widget");
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

            widgetDescriptionDynamic.Text = (currentWidget != null) ? currentWidget.GetDynamicDescription() : "No description";
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

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            base.OnPrepareOptionsMenu(menu);

            var configMenuItem = menu.FindItem(Resource.Id.BtnWidgetSettings);
            var attribs = (WidgetAttribute)(flipper.CurrentView.GetType().GetCustomAttributes(typeof(WidgetAttribute), true)[0]);

            if (configMenuItem != null)
                configMenuItem.SetEnabled(attribs.SettingsType != null);
            
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
                        HideTheRefreshText();
                    }

                    return true;

                case Resource.Id.BtnWidgetSettings:

                    var widgetName = GetWidgetNameOfCurrentlyDisplayedWidget();
                    var widgetModel = SmeedeeApp.Instance.AvailableWidgets.Single(wm => wm.Name == widgetName);

                    if (widgetModel.SettingsType != null)
                    {
                        StartActivity(new Intent(this, widgetModel.SettingsType));
                    }
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

        private void HideTheRefreshText()
        {
            var refreshText = FindViewById<TextView>(Resource.Id.RefreshText);
            refreshText.Visibility = ViewStates.Invisible;
        }

        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug("SMEEDEE", "[ REFRESHING WIDGETS ]");

            if (hasSettingsChanged)
            {
                CheckForEnabledAndDisabledWidgets();
                flipper.CurrentScreen = app.ServiceLocator.Get<IPersistenceService>().Get(CURRENT_SCREEN_PERSISTENCE_KEY, 0);
                SetCorrectTopBannerWidgetTitle();
                SetCorrectTopBannerWidgetDescription();

                Log.Debug("SMEEDEE", "Just refreshed widget list after having changed settings.");
                hasSettingsChanged = false;
            }

            RefreshAllCurrentlyEnabledWidgets();
            HideTheRefreshText();
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
            var widgetModels = SmeedeeApp.Instance.AvailableWidgets;

            var newWidgets = new List<IWidget>();
            foreach (var widgetModel in widgetModels.Where(w => w.Enabled))
            {
                var model = widgetModel;
                newWidgets.AddRange(widgets.Where(widget => widget.GetType() == model.Type));
            }

            flipper.RemoveAllViews();

            foreach (var newWidget in newWidgets)
            {
                flipper.AddView((View)newWidget);
            }
            flipper.CurrentScreen = 0;
        }

        protected override void OnPause()
        {
            base.OnPause();
            app.ServiceLocator.Get<IPersistenceService>().Save(CURRENT_SCREEN_PERSISTENCE_KEY, flipper.CurrentScreen);
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
