using System;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class Login
    {
        public const string LoginKey = "Server.Key";
        public const string LoginUrl = "Server.Url";
		public const string ValidationSuccess = "Success!";
		public const string ValidationFailed = "Failed!";

        private readonly IPersistenceService persistence;
        private SmeedeeApp app = SmeedeeApp.Instance;

        public string Key
        {
            get { return persistence.Get(LoginKey, "o8rzdNQn"); } //
            set { persistence.Save(LoginKey, value); }
        }

        public string Url
        {
            get { return NormalizeUrl(persistence.Get(LoginUrl, "http://services.smeedee.org/smeedee")); } //
            set { persistence.Save(LoginUrl, value); }
        }

        public Login()
        {
            persistence = app.ServiceLocator.Get<IPersistenceService>();
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
			Console.WriteLine(string.Format("Validating {0} : {1}", Url, Key));
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