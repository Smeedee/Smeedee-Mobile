using System;

namespace Smeedee.Model
{
    public class Login
    {
        public const string LoginKey = "Login_Key";
        public const string LoginUrl = "Login_Url";

        private IPersistenceService persistence;

        public Login()
        {
            persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
		}
		
		
		public void StoreAndValidate(string url, string key, Action<string> callback)
		{
			Key = key;
			Url = url;
			
			callback("lolo");
		}
		
		
        public bool IsValid()
        {
            //TODO: Connect with a login service, and do actual validation
            return Key != "" && Url != "";
        }

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

        private string NormalizeUrl(string url)
        {
            if (!url.EndsWith("/")) url += "/";
            if (!url.StartsWith("http")) url = "http://" + url;
            return url;
        }
    }
}
