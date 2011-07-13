namespace Smeedee
{
    public interface IMobileKVPersister
    {
        void Save(string key, string value);
        string Get(string key);
    }
}
