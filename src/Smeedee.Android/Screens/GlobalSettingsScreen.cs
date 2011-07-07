using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Application settings", Theme = "@android:style/Theme")]
    public class GlobalSettings : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.GlobalSettingsScreen);

            BindEventsToButtons();
        }

        private void BindEventsToButtons()
        {
            BindClickEventToEnabledWidgetsBtn();
            BindClickEventToServerAndUserSettingsBtn();
        }

        private void BindClickEventToEnabledWidgetsBtn()
        {
            var btnPrev = FindViewById<Button>(Resource.Id.BtnEnabledWidgets);
            btnPrev.Click += (obj, e) =>
                                 {
                                     var enabledWidgetsScreen = new Intent(this, typeof (EnabledWidgetsScreen));
                                     StartActivity(enabledWidgetsScreen);
                                 };
        }

        private void BindClickEventToServerAndUserSettingsBtn()
        {
            var btnPrev = FindViewById<Button>(Resource.Id.BtnServerAndUserSettings);
            btnPrev.Click += (obj, e) =>
            {
                var serverSettingsScreen = new Intent(this, typeof(ServerSettingsScreen));
                StartActivity(serverSettingsScreen);
            };
        }
    }
}