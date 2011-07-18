namespace Smeedee
{
    public class FakeLoginValidationService : ILoginValidationService
    {
        public bool IsValid(string url, string key)
        {
            //just require that something (whatever) is entered here, while we're faking the login
            return (url != "" && key != "");
        }
    }
}
