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
using Smeedee.Android.Screens;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "GlobalSettings", Theme = "@android:style/Theme.NoTitleBar")]
    public class GlobalSettings : Activity, View.IOnClickListener
    {
        private Button enabledWidgetBtn;
        private Button serverSettingsBtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GlobalSettingsScreen);

            enabledWidgetBtn = FindViewById<Button>(Resource.Id.BtnEnabledWidgets);
            serverSettingsBtn = FindViewById<Button>(Resource.Id.BtnServerSettings);

            enabledWidgetBtn.SetOnClickListener(this);
            serverSettingsBtn.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v == enabledWidgetBtn)
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