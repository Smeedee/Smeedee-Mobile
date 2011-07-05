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
        private ViewFlipper _flipper;
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            _flipper = FindViewById<ViewFlipper>(Resource.Id.Flipper);

            ConfigureDependencies();
            
            SetContentView(Resource.Layout.Main);

            //_flipper.AddView(new FooWidget(this));
            //_flipper.AddView(new TestWidget(this));


            //AddWidgetsToFlipper();
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
                _flipper.AddView(widget as View);
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
            btnPrev.Click += (obj, e) => _flipper.ShowPrevious();
        }
        
        private void BindNextButtonClickEvent()
        {
            var btnNext = FindViewById<Button>(Resource.Id.BtnNext);
            btnNext.Click += (sender, args) => _flipper.ShowNext();
        }
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            var btnWidgetSettings = FindViewById(Resource.Id.BtnWidgetSettings);
            var btnGlobalSettings = FindViewById(Resource.Id.BtnGlobalSettings);

            if (item == btnWidgetSettings)
            {
                //TODO: Open current widget settings view
                var currentWidget = _flipper.CurrentView;
                return true;
            }
            if (item == btnGlobalSettings)
            {
                //TODO: Open Global Settings view/activity
                return true;
            } 
            
            return base.OnOptionsItemSelected(item);
        }
    }
}
