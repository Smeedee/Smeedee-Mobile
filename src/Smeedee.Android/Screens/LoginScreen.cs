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
using Smeedee.Services;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "LoginScreen", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
    public class LoginScreen : Activity
    {
        private ILoginValidationService loginValidator;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LoginScreen);

            loginValidator = new FakeLoginValidationService();

            var submitButton = FindViewById<Button>(Resource.Id.LoginButton);
            var urlInput = FindViewById<EditText>(Resource.Id.ServerUrlInput);
            var keyInput = FindViewById<EditText>(Resource.Id.AccessKeyInput);
            submitButton.Click += delegate(object sender, EventArgs args)
                {
                    if (loginValidator.IsValid(urlInput.Text, keyInput.Text))
                    {
                        nextScreen();
                    } else
                    {
                        notifyInvalidInput();
                    }
                };
        }

        private void nextScreen()
        {
            
        }

        private void notifyInvalidInput()
        {
            var errorNotifier = FindViewById<TextView>(Resource.Id.ErrorNotificationText);
            errorNotifier.Visibility = ViewStates.Visible;
        }
    }
}