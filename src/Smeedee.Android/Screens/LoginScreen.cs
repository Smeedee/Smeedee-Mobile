using Android.App;
using Android.Content;
using Android.OS;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Please enter login information", Theme = "@android:style/Theme.Dialog")]
    public class LoginScreen : Activity
    {
        private IBackgroundWorker _bgWorker;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);

            _bgWorker = SmeedeeApp.Instance.ServiceLocator.Get<IBackgroundWorker>();
            var submitButton = FindViewById<Button>(Resource.Id.BtnLogin);
            var urlInput = FindViewById<EditText>(Resource.Id.LoginScreenServerUrlInput);
            var keyInput = FindViewById<EditText>(Resource.Id.LoginScreenUserKeyInput);

            var login = new Login();
            urlInput.Text = login.Url;
            keyInput.Text = login.Key;

            submitButton.Click += delegate
                {
                    var dialog = ProgressDialog.Show(this, "Please wait...", "Connecting to server and validating key...", true);
                    var handler = new ProgressHandler(dialog);

                    login.StoreAndValidate(urlInput.Text, keyInput.Text, (valid) => RunOnUiThread(() =>
                        {
                            if (valid == Login.ValidationSuccess)
                            {
                                dialog.SetMessage("Successfully connected to " + urlInput.Text);
                                var widgetContainer = new Intent(this, typeof(WidgetContainer));
                                StartActivity(widgetContainer);
                                Finish();
                                handler.SendEmptyMessage(0);
                            } else
                            {
                                dialog.SetMessage("Connection failed. Please try again");
                            
                                NotifyInvalidInput();
                                handler.SendEmptyMessage(0);
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