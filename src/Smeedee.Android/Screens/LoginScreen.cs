using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Please enter server url and key", Theme = "@android:style/Theme.Dialog")]
    public class LoginScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);

            var submitButton = FindViewById<Button>(Resource.Id.BtnLogin);
            var urlInput = FindViewById<EditText>(Resource.Id.LoginScreenServerUrlInput);
            var keyInput = FindViewById<EditText>(Resource.Id.LoginScreenUserKeyInput);

            urlInput.Text = Login.DefaultSmeedeeUrl;
            keyInput.Text = Login.DefaultSmeedeeKey;

            submitButton.Click += delegate
                {
                    var dialog = ProgressDialog.Show(this, "", "Connecting to server and validating key...", true);
                    var handler = new ProgressHandler(dialog);

                    new Login().ValidateAndStore(urlInput.Text, keyInput.Text, (valid) => RunOnUiThread(() =>
                        {
                            if (valid == Login.ValidationSuccess)
                            {
                                dialog.SetMessage("Successfully connected to " + urlInput.Text);
                                var startUpLoadingScreen = new Intent(this, typeof(StartUpLoadingScreen));
                                StartActivity(startUpLoadingScreen);
                                Finish();
                                handler.SendEmptyMessage(0);
                            } else
                            {
                                dialog.SetMessage("Connection failed. Please try again");
                                handler.SendEmptyMessage(0);
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