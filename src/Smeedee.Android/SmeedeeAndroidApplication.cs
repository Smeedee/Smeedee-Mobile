using System;
using Android.App;
using Android.Util;
using Smeedee.Model;

namespace Smeedee.Android
{
    [Application]
    public class SmeedeeAndroidApplication : Application
    {
        public SmeedeeApp App { get; private set; }

        public SmeedeeAndroidApplication(IntPtr handle) : base(handle)
        {
            App = SmeedeeApp.Instance;
        }

        public override void OnCreate()
        {
            base.OnCreate();
            Log.Debug("TT", "Application is being run");
        }
    }
}