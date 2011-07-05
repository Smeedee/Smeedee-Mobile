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
    [Activity(Label = "GlobalSettings")]
    public class GlobalSettings : Activity, View.IOnClickListener
    {
        private Button enabledWidgetsBtn;
        private Button serverSettingsBtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.GlobalSettings);

            enabledWidgetsBtn = FindViewById<Button>(Resource.Id.BtnEnabledWidgets);
            serverSettingsBtn = FindViewById<Button>(Resource.Id.BtnServerSettings);
        }

        public void OnClick(View v)
        {
            if (v == enabledWidgetsBtn)
            {
                var enabledWidgetsScreen = new Intent(this, typeof(EnabledWidgetsScreen));
                StartActivity(enabledWidgetsScreen);
            }
            if (v == serverSettingsBtn)
            {
                var serverSettingsScreen = new Intent(this, typeof(ServerSettingsScreen));
                StartActivity(serverSettingsScreen);
            }
        }
    }
}