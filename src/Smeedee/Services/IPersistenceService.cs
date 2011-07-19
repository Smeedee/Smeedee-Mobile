namespace Smeedee
{
    public interface IPersistenceService
    {
        void Save(string key, string value);
        string Get(string key, string defaultValue);
        void Save(string key, bool value);
        bool Get(string key, bool defaultValue);
    }
}
