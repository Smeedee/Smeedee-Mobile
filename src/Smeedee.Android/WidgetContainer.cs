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
    [Activity(Label = "Smeedee.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class WidgetContainer : Activity
    {
        private SmeedeeApp app = SmeedeeApp.Instance;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            ConfigureDependencies();
            
            SetContentView(Resource.Layout.Main);
            AddWidgetsToFlipper();
            SetNextButtonText();
            BindEventsToNavigationButtons();
        }

        private void ConfigureDependencies()
        {
            SmeedeeApp.SmeedeeService = new SmeedeeFakeService();
        }
        
        private void AddWidgetsToFlipper()
        {
            var flipper = FindViewById<ViewFlipper>(Resource.Id.Flipper);

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
  
        private void SetNextButtonText()
        {
            var nextButtonText = FindViewById<Button>(Resource.Id.BtnNext);
            nextButtonText.Text = widgets.Count() + " w";
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
            btnNext.Click += delegate
                                 {
                                     flipper.ShowNext();
                                 };
        }
        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }
    }
}
