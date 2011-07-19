using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee;
using Smeedee.Android.Services;
using Smeedee.Model;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Please enter login information", Theme = "@android:style/Theme.Dialog")]
    public class LoginScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);

            var login = new Login();

            var submitButton = FindViewById<Button>(Resource.Id.BtnLogin);
            var urlInput = FindViewById<EditText>(Resource.Id.ServerUrlInput);
            var keyInput = FindViewById<EditText>(Resource.Id.UserPasswordInput);
            submitButton.Click += delegate
                {
                    login.Url = urlInput.Text;
                    login.Key = keyInput.Text;
                    if (login.IsValid())
                    {
                        var widgetContainer = new Intent(this, typeof(WidgetContainer));
                        StartActivity(widgetContainer);
                    } else
                    {
                        NotifyInvalidInput();
                    }
                };
        }

        private void NotifyInvalidInput()
        {
            var errorNotifier = FindViewById<TextView>(Resource.Id.ErrorNotificationText);
            errorNotifier.Visibility = ViewStates.Visible;
        }
    }
}