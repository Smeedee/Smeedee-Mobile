using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class Activity1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            ConfigureDependencies();
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

