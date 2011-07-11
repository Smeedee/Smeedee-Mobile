using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using Android.OS;
using Smeedee.Android.Screens;
using Smeedee.Android.Widgets;
using Smeedee.Android.Widgets.Settings;
using Smeedee.Model;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee Mobile", Theme = "@android:style/Theme.NoTitleBar")]
    public class WidgetContainer : Activity
    {
        private SmeedeeApp app = SmeedeeApp.Instance;
        private ViewFlipper _flipper;
        private View _currentDisplayingView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            _flipper = FindViewById<ViewFlipper>(Resource.Id.Flipper);
            
            AddWidgetsToFlipper();
            SetCorrectTopBannerWidgetTitle();
            SetCorrectTopBannerWidgetDescription();
            BindEventsToNavigationButtons();
        }

        private void AddWidgetsToFlipper()
        {
            var widgets = GetWidgets();
            foreach (var widget in widgets)
            {
                _flipper.AddView(widget as View);
            }
            _currentDisplayingView = _flipper.CurrentView;
        }

        private IEnumerable<IWidget> GetWidgets()
        {
            app.RegisterAvailableWidgets();

            var widgets = SmeedeeApp.Instance.AvailableWidgets;
            var instances = new List<IWidget>();
            foreach (var widget in widgets)
            {
                instances.Add(Activator.CreateInstance(widget.Type, this) as IWidget);
            }
            return instances;
        }

        private void SetCorrectTopBannerWidgetTitle()
        {
            var widgetTitle = FindViewById<TextView>(Resource.Id.WidgetNameInTopBanner);
            var widgetNameFromAttribute = "widget name not set";
            var widgets = SmeedeeApp.Instance.AvailableWidgets;
            foreach (var widgetModel in widgets)
            {
                if (_currentDisplayingView.GetType() == widgetModel.Type)
                {
                    var widgetAttributes = (WidgetAttribute[])widgetModel.Type.GetCustomAttributes(typeof(WidgetAttribute), true);
                    widgetNameFromAttribute = widgetAttributes[0].Name;
                }
            }
            widgetTitle.Text = widgetNameFromAttribute;
        }

        private void SetCorrectTopBannerWidgetDescription()
        {
            var widgetDescriptionDynamic = FindViewById<TextView>(Resource.Id.WidgetDynamicDescriptionInTopBanner);
            var widgetDescriptionFromAttribute = "";
            var widgets = SmeedeeApp.Instance.AvailableWidgets;
            foreach (var widgetModel in widgets)
            {
                if (_currentDisplayingView.GetType() == widgetModel.Type)
                {
                    var widgetAttributes = (WidgetAttribute[])widgetModel.Type.GetCustomAttributes(typeof(WidgetAttribute), true);
                    widgetDescriptionFromAttribute = widgetAttributes[0].DescriptionStatic;
                }
            }
            widgetDescriptionDynamic.Text = widgetDescriptionFromAttribute;
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
                                     _flipper.ShowPrevious();
                                     SetCorrectTopBannerWidgetTitle();
                                     SetCorrectTopBannerWidgetDescription();
                                     _flipper.RefreshDrawableState();
                                 };
        }

        private void BindNextButtonClickEvent()
        {
            var btnNext = FindViewById<Button>(Resource.Id.BtnNext);
            btnNext.Click += (sender, args) =>
                                 {
                                     _flipper.ShowNext();
                                     SetCorrectTopBannerWidgetTitle();
                                     SetCorrectTopBannerWidgetDescription();
                                     _flipper.RefreshDrawableState();
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
                case Resource.Id.BtnWidgetSettings:

                    // TODO: Open current widget settings view
                    // var currentWidget = _flipper.CurrentView;

                    var settingsIntent = new Intent(this, typeof (TopCommittersSettings));
                    StartActivity(settingsIntent);
                    return true;

                case Resource.Id.BtnGlobalSettings:

                    var globalSettings = new Intent(this, typeof(GlobalSettings));
                    StartActivity(globalSettings);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
