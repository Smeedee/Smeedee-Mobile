using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Android.OS;
using Smeedee.Android.Screens;
using Smeedee.Android.Widgets.Settings;
using Smeedee.Model;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee Mobile", Theme = "@android:style/Theme.NoTitleBar")]
    public class WidgetContainer : Activity
    {
        private SmeedeeApp app = SmeedeeApp.Instance;
        private ViewFlipper _flipper;

        private IEnumerable<IWidget> widgets;

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
            widgets = GetWidgets();
            foreach (var widget in widgets)
            {
                _flipper.AddView(widget as View);
            }
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
            widgetTitle.Text = GetWidgetAttribute("Name");
        }

        private void SetCorrectTopBannerWidgetDescription()
        {
            var widgetDescriptionDynamic = FindViewById<TextView>(Resource.Id.WidgetDynamicDescriptionInTopBanner);
            widgetDescriptionDynamic.Text = GetWidgetAttribute("DescriptionStatic");
        }

        private string GetWidgetAttribute(string attribute)
        {
            var setAttribute = "not set";
            var widgets = SmeedeeApp.Instance.AvailableWidgets;
            foreach (var widgetModel in widgets)
            {
                if (_flipper.CurrentView.GetType() == widgetModel.Type)
                {
                    var widgetAttributes =
                        (WidgetAttribute[]) widgetModel.Type.GetCustomAttributes(typeof (WidgetAttribute), true);
                    if (attribute == "Name") setAttribute = widgetAttributes[0].Name;
                    if (attribute == "DescriptionStatic") setAttribute = widgetAttributes[0].DescriptionStatic;
                }
            }
            return setAttribute;
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

                    // TODO: Make dynamic or something :)
                    if (GetWidgetAttribute("Name") == "Build Status")
                        StartActivity(new Intent(this, typeof(BuildStatusSettings)));

                    return true;

                case Resource.Id.BtnGlobalSettings:

                    var globalSettings = new Intent(this, typeof(GlobalSettings));
                    StartActivity(globalSettings);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }

        protected override void OnResume()
        {
            base.OnResume();
            Log.Debug("TT", "[REFRESHING WIDGETS]");
            foreach (var widget in widgets)
            {
                widget.Refresh();
            }
        }
    }
}
