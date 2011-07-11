using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using Smeedee.Services;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Login", Theme = "@android:style/Theme")]
    public class LoginScreen : Activity
    {
        private ILoginValidationService _loginValidator;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);

            _loginValidator = new FakeLoginValidationService();

            var submitButton = FindViewById<Button>(Resource.Id.BtnLogin);
            var urlInput = FindViewById<EditText>(Resource.Id.ServerUrlInput);
            var keyInput = FindViewById<EditText>(Resource.Id.AccessKeyInput);
            submitButton.Click += delegate
                {
                    if (_loginValidator.IsValid(urlInput.Text, keyInput.Text))
                    {
                        var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
                        var editor = prefs.Edit();
                        editor.PutString("serverUrl", urlInput.Text);
                        editor.PutString("userPassword", keyInput.Text);

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