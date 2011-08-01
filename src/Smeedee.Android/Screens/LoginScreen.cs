using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
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

            var submitButton = FindViewById<Button>(Resource.Id.BtnLogin);
            var urlInput = FindViewById<EditText>(Resource.Id.LoginScreenServerUrlInput);
            var keyInput = FindViewById<EditText>(Resource.Id.LoginScreenUserKeyInput);

            var login = new Login();
            urlInput.Text = login.Url;
            keyInput.Text = login.Key;

            submitButton.Click += delegate
                {
                    login.StoreAndValidate(urlInput.Text, keyInput.Text, (valid) => RunOnUiThread(()=> {
                        if (valid == Login.ValidationSuccess)
                        {
                            var widgetContainer = new Intent(this, typeof(WidgetContainer));
                            StartActivity(widgetContainer);
                            Finish();
                        } else
                        {
                            NotifyInvalidInput();
                        }
                    }));
                };
        }

        private void NotifyInvalidInput()
        {
            var errorNotifier = FindViewById<TextView>(Resource.Id.ErrorNotificationText);
            if (errorNotifier != null)
                errorNotifier.Visibility = ViewStates.Visible;
        }
    }
}