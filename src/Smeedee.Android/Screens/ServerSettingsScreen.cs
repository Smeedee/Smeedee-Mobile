using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Server and user settings", Theme = "@android:style/Theme")]
    public class ServerSettingsScreen : Activity
    {
        private ILoginValidationService _loginValidator;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.ServerSettingsScreen);

            _loginValidator = new FakeLoginValidationService();

            BindClickEventToSaveBtn();
            BindClickEventToCancelBtn();
        }

        private void BindClickEventToSaveBtn()
        {
            var saveBtn = FindViewById<Button>(Resource.Id.BtnSaveServerAndUserSettings);
            var urlInputText = FindViewById<EditText>(Resource.Id.ServerUrlInput);
            var keyInputText = FindViewById<EditText>(Resource.Id.AccessKeyInput);
            SetTextFieldHints(keyInputText, urlInputText);

            saveBtn.Click += delegate
                                 {
                                     if (_loginValidator.IsValid(urlInputText.Text, keyInputText.Text))
                                     {
                                         var globalSettings = new Intent(this, typeof (GlobalSettings));
                                         StartActivity(globalSettings);
                                     }
                                     else
                                     {
                                         NotifyInvalidInput();
                                     }
                                 };
        }

        private static void SetTextFieldHints(EditText keyInputText, EditText urlInputText)
        {
            urlInputText.Hint = SmeedeeApp.Instance.GetStoredLoginUrl();
            keyInputText.Hint = SmeedeeApp.Instance.GetStoredLoginKey();
        }

        private void NotifyInvalidInput()
        {
            var errorNotifier = FindViewById<TextView>(Resource.Id.ErrorNotificationText);
            errorNotifier.Visibility = ViewStates.Visible;
        }
        private void BindClickEventToCancelBtn()
        {
            var cancelBtn = FindViewById<Button>(Resource.Id.BtnCancelServerAndUserSettings);
            
            cancelBtn.Click += delegate
                                   {
                                       var globalSettings = new Intent(this, typeof (GlobalSettings));
                                       StartActivity(globalSettings);
                                       Finish();
                                   };
        }
    }
}