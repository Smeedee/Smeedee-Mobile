namespace Smeedee.Services
{
    public interface ILoginValidationService
    {
        bool IsValid(string url, string key);
    }
}