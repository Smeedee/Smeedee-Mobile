﻿using System;
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
using Smeedee.Android.Widgets.Settings;
using Smeedee.Model;
using Exception = System.Exception;

namespace Smeedee.Android
{
    [Activity(
        Label = "Smeedee Mobile",
        Theme = "@android:style/Theme.NoTitleBar",
        ConfigurationChanges = ConfigChanges.KeyboardHidden | ConfigChanges.Orientation)]
    public class WidgetContainer : Activity
    {
        private const int SCROLL_NEXT_VIEW_THRESHOLD = 100; // TODO: Make dynamic based on screen size?
        private readonly SmeedeeApp app = SmeedeeApp.Instance;
        private ViewFlipper flipper;
        private IEnumerable<IWidget> widgets;

        private ISharedPreferencesOnSharedPreferenceChangeListener preferenceChangeListener;
        private bool hasSettingsChange;
        private double oldTouchValue;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            flipper = FindViewById<ViewFlipper>(Resource.Id.Flipper);

            AddWidgetsToFlipper();
            SetCorrectTopBannerWidgetTitle();
            SetCorrectTopBannerWidgetDescription();
            BindEventsToNavigationButtons();

            preferenceChangeListener = new SharedPreferencesChangeListener(() =>
                                                                               {
                                                                                   hasSettingsChange = true;
                                                                               });
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            prefs.RegisterOnSharedPreferenceChangeListener(preferenceChangeListener);
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

            if (currentWidget != null)
            {
                currentWidget.Refresh();
                widgetDescriptionDynamic.Text = currentWidget.GetDynamicDescription();
            }
            else
            {
                throw new NullReferenceException("Could not set the dynamic description because there " +
                                                 "where no current widget in viewflipper");
            }
        }

        private string GetWidgetNameOfCurrentlyDisplayedWidget()
        {
            var name = "";
            foreach (var widgetModel in SmeedeeApp.Instance.AvailableWidgets)
            {
                if (flipper.CurrentView.GetType() == widgetModel.Type)
                    name = widgetModel.Name;
            }
            return name;
        }

        private void BindEventsToNavigationButtons()
        {
            BindPreviousButtonClickEvent();
            BindNextButtonClickEvent();
        }

        private void BindPreviousButtonClickEvent()
        {
            var btnPrev = FindViewById<Button>(Resource.Id.BtnPrev);
            btnPrev.Click += (obj, e) =>
                                 {
                                     flipper.ShowPrevious();
                                     SetCorrectTopBannerWidgetTitle();
                                     SetCorrectTopBannerWidgetDescription();
                                     flipper.RefreshDrawableState();
                                 };
        }

        private void BindNextButtonClickEvent()
        {
            var btnNext = FindViewById<Button>(Resource.Id.BtnNext);
            btnNext.Click += (sender, args) =>
                                 {
                                     flipper.ShowNext();
                                     SetCorrectTopBannerWidgetTitle();
                                     SetCorrectTopBannerWidgetDescription();
                                     flipper.RefreshDrawableState();
                                 };
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
                    else
                        throw new NullReferenceException("Could not refresh the widget because there where" +
                                                         "no current widget in viewflipper");

                    return true;

                case Resource.Id.BtnWidgetSettings:

                    string widgetName = GetWidgetNameOfCurrentlyDisplayedWidget();

                    if (widgetName == "Build Status")
                        StartActivity(new Intent(this, typeof(BuildStatusSettings)));

                    if (widgetName == "Top Committers")
                        StartActivity(new Intent(this, typeof(TopCommittersSettings)));

                    if (widgetName == "Latest Changesets")
                        StartActivity(new Intent(this, typeof(LatestChangesetsSettings)));

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

            if (hasSettingsChange)
            {
                CheckForEnabledAndDisabledWidgets();
                SetCorrectTopBannerWidgetTitle();
                SetCorrectTopBannerWidgetDescription();
                Log.Debug("TT", "Just refreshed widget list after having changed settings.");
                hasSettingsChange = false;
            }

            foreach (var widget in widgets)
            {
                widget.Refresh();
            }
        }

        private void CheckForEnabledAndDisabledWidgets()
        {
            var widgetModels = SmeedeeApp.Instance.AvailableWidgets;

            var newWidgets = new List<IWidget>();
            foreach (var widgetModel in widgetModels.Where(WidgetIsEnabled))
            {
                newWidgets.AddRange(widgets.Where(widget => widget.GetType() == widgetModel.Type));
            }

            var current = flipper.DisplayedChild;
            flipper.RemoveAllViews();

            foreach (var newWidget in newWidgets)
            {
                flipper.AddView((View)newWidget);
            }
            flipper.DisplayedChild = current;
        }

        private bool WidgetIsEnabled(WidgetModel widget)
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            return prefs.GetBoolean(widget.Name, true);
        }

        public override bool OnTouchEvent(MotionEvent touchEvent)
        {
            switch (touchEvent.Action)
            {
                case MotionEventActions.Down:
                    oldTouchValue = touchEvent.GetX();
                    break;

                case MotionEventActions.Up:
                    float currentX = touchEvent.GetX();
                    if (oldTouchValue < currentX-SCROLL_NEXT_VIEW_THRESHOLD)
                    {
                        flipper.ShowNext();
                    } else if (oldTouchValue > currentX+SCROLL_NEXT_VIEW_THRESHOLD)
                    {
                        flipper.ShowPrevious();
                    } else
                    {
                        var cur = flipper.CurrentView;
                        cur.Layout(0, cur.Top, cur.Bottom, cur.Right);
                    }
                    break;

                case MotionEventActions.Move:
                    var currentView = flipper.CurrentView;
                    currentView.Layout((int)(touchEvent.GetX() - oldTouchValue),
                    currentView.Top, currentView.Right,
                    currentView.Bottom);

                    //var nextView = flipper.GetChildAt(flipper.DisplayedChild - 1);
                    //var previousView = flipper.GetChildAt(flipper.DisplayedChild + 1);

                    //nextView.Layout(currentView.Right, nextView.Top, nextView.Right, nextView.Bottom);
                    //previousView.Layout(previousView.Right, previousView.Top, currentView.Left, previousView.Bottom);


                    break;
            }
            return false; // True if the event was handled, false otherwise. Leave false to propagate event further?
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
