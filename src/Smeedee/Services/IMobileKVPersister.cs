namespace Smeedee.Services
{
    public interface IMobileKVPersister
    {
        void Save(string key, string value);
        string Get(string key);
    }
}
