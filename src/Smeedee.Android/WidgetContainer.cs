using System;
using System.Collections.Generic;
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
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var layout = FindViewById<LinearLayout>(Resource.Id.ContainerLayout);

            var widgets = GetWidgets();
            foreach (var widget in widgets)
            {
                layout.AddView(widget as View);
            }


            ConfigureDependencies();
        }

        private IEnumerable<IWidget> GetWidgets()
        {
            return new IWidget[] {new TestWidget(null)};
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.Main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            return base.OnOptionsItemSelected(item);
        }

        private void ConfigureDependencies()
        {
            SmeedeeApp.SmeedeeService = new SmeedeeFakeService();
            RegisterAllSupportedWidgets();
        }

        private void RegisterAllSupportedWidgets()
        {
            
        }
    }
}

