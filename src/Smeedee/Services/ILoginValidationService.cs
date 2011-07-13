namespace Smeedee
{
    public interface ILoginValidationService
    {
        bool IsValid(string url, string key);
    }
}