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
            get { return persistence.Get(LoginUrl, ""); }
            set { persistence.Save(LoginUrl, value); }
        }
    }
}
