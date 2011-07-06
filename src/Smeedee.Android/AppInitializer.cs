﻿using System;
using System.Collections.Generic;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Smeedee.Android.Screens;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
    public class AppInitializer : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ConfigureDependencies();
            
            var activity = DetermineNextActivity();

            StartActivity(activity);
        }

        private Intent DetermineNextActivity()
        {
            Type nextActivity = HasAValidUrlAndKey()
                                    ? typeof (WidgetContainer)
                                    : typeof (LoginScreen);
            return new Intent(this, nextActivity);
        }

        private bool HasAValidUrlAndKey()
        {
            var key = SmeedeeApp.Instance.GetStoredLoginKey();
            var url = SmeedeeApp.Instance.GetStoredLoginUrl();
            if (url == null || key == null) return false;

            var validator = SmeedeeApp.Instance.ServiceLocator.Get<ILoginValidationService>();
            return validator.IsValid(url, key);
        }

        private void ConfigureDependencies()
        {
            var serviceLocator = SmeedeeApp.Instance.ServiceLocator;

            //fill in global bindings here:
            serviceLocator.Bind<ILoginValidationService>(new FakeLoginValidationService());
            serviceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftFakeService());
            serviceLocator.Bind<ISmeedeeService>(new SmeedeeHttpService());
        }
    }
}
