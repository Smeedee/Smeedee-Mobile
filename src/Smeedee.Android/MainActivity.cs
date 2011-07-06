using System;
using Android.App;
using Android.Content;
using Android.OS;
using Smeedee.Android.Screens;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android
{
    [Activity(Label = "Smeedee", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            ConfigureDependencies();
            
            var activity = DetermineNextActivity();
            StartActivity(activity);
        }

        private void ConfigureDependencies()
        {
            var serviceLocator = SmeedeeApp.Instance.ServiceLocator;

            // Fill in global bindings here:
            serviceLocator.Bind<ISmeedeeService>(new SmeedeeFakeService());
            serviceLocator.Bind<IWorkingDaysLeftService>(new WorkingDaysLeftFakeService());
            serviceLocator.Bind<ILoginValidationService>(new FakeLoginValidationService());
            serviceLocator.Bind<ISmeedeeService>(new SmeedeeHttpService());
        }

        private Intent DetermineNextActivity()
        {
            Type nextActivity = UserHasAValidUrlAndKey()
                                    ? typeof (WidgetContainer)
                                    : typeof (LoginScreen);
            return new Intent(this, nextActivity);
        }

        private bool UserHasAValidUrlAndKey()
        {
            var key = SmeedeeApp.Instance.GetStoredLoginKey();
            var url = SmeedeeApp.Instance.GetStoredLoginUrl();
            if (url == null || key == null) return false;

            var validator = SmeedeeApp.Instance.ServiceLocator.Get<ILoginValidationService>();
            return validator.IsValid(url, key);
        }
    }
}
