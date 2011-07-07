using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Server and user settings", Theme = "@android:style/Theme")]
    public class ServerSettingsScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var view = FindViewById<TextView>(Resource.Id.ServerSettingsScreenTextView);
            SetContentView(view);
        }
    }
}