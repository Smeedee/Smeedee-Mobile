using System;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class Login
    {
        public const string LoginKey = "Login_Key";
        public const string LoginUrl = "Login_Url";
		public const string ValidationSuccess = "Success!";
		public const string ValidationFailed = "Failed!";

        private IPersistenceService persistence;
		private IValidationService validation;
        
        public string Key
        {
            get { return persistence.Get(LoginKey, ""); }
            set { persistence.Save(LoginKey, value); }
        }

        public string Url
        {
            get { return NormalizeUrl(persistence.Get(LoginUrl, "")); }
            set { persistence.Save(LoginUrl, value); }
        }

        public Login()
        {
            persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
			validation = SmeedeeApp.Instance.ServiceLocator.Get<IValidationService>();
		}
		
		public void StoreAndValidate(string url, string key, Action<string> callback)
		{
			Key = key;
			Url = url;

			validation.Validate(url, key, (validationSuccess) => 
			{
				if (validationSuccess) callback(ValidationSuccess);
				else callback(ValidationFailed);
			});
		}
		
        public bool IsValid()
        {
            var validationResult = false;
            validation.Validate(Url, Key, (result) => validationResult = result);
            return validationResult;
        }

        private string NormalizeUrl(string url)
        {
            if (!url.StartsWith("http")) url = "http://" + url;
            if (!url.EndsWith("/")) url += "/";
            return url;
        }
    }
}