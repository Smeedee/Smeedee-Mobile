using System;
using Android.Util;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class Login
    {
        public const string LoginKey = "Login_Key";
        public const string LoginUrl = "Login_Url";
		public const string ValidationSuccess = "Success!";
		public const string ValidationFailed = "Failed!";

        private readonly IPersistenceService _persistence;
        private SmeedeeApp app = SmeedeeApp.Instance;

        public string Key
        {
            get { return _persistence.Get(LoginKey, ""); } //
            set { _persistence.Save(LoginKey, value); }
        }

        public string Url
        {
            get { return NormalizeUrl(_persistence.Get(LoginUrl, "")); } //
            set { _persistence.Save(LoginUrl, value); }
        }

        public Login()
        {
            _persistence = app.ServiceLocator.Get<IPersistenceService>();

            // Remove this later on
            Key = "o8rzdNQn";
            Url = "http://services.smeedee.org/smeedee/";
        }
		
		public void StoreAndValidate(string url, string key, Action<string> callback)
        {
			Key = key;
			Url = url;
			IsValid(validationSuccess => 
			{
				if (validationSuccess) callback(ValidationSuccess);
				else callback(ValidationFailed);
			});
		}

        public void IsValid(Action<bool> callback)
        {
            var validation = app.ServiceLocator.Get<IValidationService>();
            validation.Validate(Url, Key, callback);
        }

        private string NormalizeUrl(string url)
        {
            if (!url.StartsWith("http")) url = "http://" + url;
            if (!url.EndsWith("/")) url += "/";
            return url;
        }
    }
}