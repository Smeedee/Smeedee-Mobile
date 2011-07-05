using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Smeedee.Android.Widgets;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee Mobile", MainLauncher = true, Icon = "@drawable/icon", Theme = "@android:style/Theme.NoTitleBar")]
    public class WidgetContainer : Activity
    {
        private SmeedeeApp app = SmeedeeApp.Instance;
        private ViewFlipper flipper;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            flipper = FindViewById<ViewFlipper>(Resource.Id.Flipper);
            
            ConfigureDependencies();
            
            AddWidgetsToFlipper();
            BindEventsToNavigationButtons();
        }

        private void ConfigureDependencies()
        {
            SmeedeeApp.Instance.ServiceLocator.Bind<ISmeedeeService>(new SmeedeeFakeService());
        }
        
        private void AddWidgetsToFlipper()
        {
            var widgets = GetWidgets();
            foreach (var widget in widgets)
            {
                flipper.AddView(widget as View);
            }
        }

        private IEnumerable<IWidget> GetWidgets()
        {
            app.RegisterAvailableWidgets();

            var widgetTypes = SmeedeeApp.Instance.AvailableWidgetTypes;
            var instances = new List<IWidget>();
            foreach (var widgetType in widgetTypes)
            {
                instances.Add(Activator.CreateInstance(widgetType, this) as IWidget);
            }
            return instances;
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
                                     flipper.RefreshDrawableState();
                                 };
        }
        
        private void BindNextButtonClickEvent()
        {
            var btnNext = FindViewById<Button>(Resource.Id.BtnNext);
            btnNext.Click += (sender, args) =>
                                 {
                                     flipper.ShowNext();
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
                case Resource.Id.BtnWidgetSettings:

                    // TODO: Open current widget settings view
                    // var currentWidget = flipper.CurrentView;
                    return true;

                case Resource.Id.BtnGlobalSettings:

                    // TODO: What happens if a user clicks multiple times on Global Settings?
                    // Will multiple activities start, or is Android so smart that is just uses the one
                    // it has got from before
                    var globalSettings = new Intent(this, typeof (GlobalSettings));
                    StartActivity(globalSettings);
                    return true;

                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}
