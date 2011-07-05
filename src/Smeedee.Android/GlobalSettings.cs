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

namespace Smeedee.Android
{
    [Activity(Label = "GlobalSettings", Theme = "@android:style/Theme.NoTitleBar")]
    public class GlobalSettings : Activity, View.IOnClickListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GlobalSettings);
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                case Resource.Id.BtnEnabledWidgets:

                    var enabledWidgetsScreen = new Intent(this, typeof(EnabledWidgetsScreen));
                    StartActivity(enabledWidgetsScreen);
                    break;

                case Resource.Id.BtnServerSettings:
                    var serverSettingsScreen = new Intent(this, typeof(ServerSettingsScreen));
                    StartActivity(serverSettingsScreen);
                    break;
        }
    }
}