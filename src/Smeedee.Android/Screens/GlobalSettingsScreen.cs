using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "GlobalSettings", Theme = "@android:style/Theme.NoTitleBar")]
    public class GlobalSettings : Activity, View.IOnClickListener
    {
        private Button _enabledWidgetBtn;
        private Button _serverSettingsBtn;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GlobalSettingsScreen);

            _enabledWidgetBtn = FindViewById<Button>(Resource.Id.BtnEnabledWidgets);
            _serverSettingsBtn = FindViewById<Button>(Resource.Id.BtnServerSettings);

            _enabledWidgetBtn.SetOnClickListener(this);
            _serverSettingsBtn.SetOnClickListener(this);
        }

        public void OnClick(View v)
        {
            if (v == _enabledWidgetBtn)
            {
                var enabledWidgetsScreen = new Intent(this, typeof(EnabledWidgetsScreen));
                StartActivity(enabledWidgetsScreen);
            }
            if (v == _serverSettingsBtn)
            {
                var serverSettingsScreen = new Intent(this, typeof(ServerSettingsScreen));
                StartActivity(serverSettingsScreen);
            }
        }
    }
}