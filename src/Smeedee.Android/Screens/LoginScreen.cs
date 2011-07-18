using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Views;
using Android.Widget;
using Smeedee;
using Smeedee.Android.Services;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Please enter login information", Theme = "@android:style/Theme.Dialog")]
    public class LoginScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);

            var loginValidator = new FakeLoginValidationService();

            var submitButton = FindViewById<Button>(Resource.Id.BtnLogin);
            var urlInput = FindViewById<EditText>(Resource.Id.ServerUrlInput);
            var userPasswordInput = FindViewById<EditText>(Resource.Id.UserPasswordInput);
            submitButton.Click += delegate
                {
                    if (loginValidator.IsValid(urlInput.Text, userPasswordInput.Text))
                    {
                        var prefEditor = PreferenceManager.GetDefaultSharedPreferences(this).Edit();
                        prefEditor.PutString("severUrl", urlInput.Text);
                        prefEditor.PutString("userPassword", userPasswordInput.Text);
                        prefEditor.Commit();

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