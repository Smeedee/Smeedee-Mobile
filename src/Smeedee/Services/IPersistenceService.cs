namespace Smeedee
{
    public interface IPersistenceService
    {
        void Save(string key, string value);
        string Get(string key, string defaultValue);
    }
}
