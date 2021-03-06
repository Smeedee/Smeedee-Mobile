﻿using System;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class Login
    {
        public const string LoginKey = "Server.Key";
        public const string LoginUrl = "Server.Url";
		public const string ValidationSuccess = "Success!";
		public const string ValidationFailed = "Failed!";
		
		public const string DefaultSmeedeeUrl = "http://services.smeedee.org/smeedee/";
		public const string DefaultSmeedeeKey = "o8rzdNQn";

        private readonly IPersistenceService _persistence;
        private SmeedeeApp app = SmeedeeApp.Instance;

        public string Key
        {
            get { return _persistence.Get(LoginKey, ""); }
            set { _persistence.Save(LoginKey, value); }
        }

        public string Url
        {
            get { return _persistence.Get(LoginUrl, ""); }
            set { _persistence.Save(LoginUrl, NormalizeUrl(value)); }
        }

        public Login()
        {
            _persistence = app.ServiceLocator.Get<IPersistenceService>();
        }
		
		public void ValidateAndStore(string url, string key, Action<string> callback)
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

        private static string NormalizeUrl(string url)
        {
            if (!url.StartsWith("http")) url = "http://" + url;
            if (!url.EndsWith("/")) url += "/";
            return url;
        }
    }
}