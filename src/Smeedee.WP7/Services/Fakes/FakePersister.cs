using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.WP7.Services.Fakes
{
    public class FakePersister : IPersistenceService
    {
        private Dictionary<string, string> strings = new Dictionary<string, string>()
                                                         {
                                                             {Login.LoginKey, "o8rzdNQn"},
                                                             {Login.LoginUrl, "http://services.smeedee.org/smeedee/"}
                                                         }; 
        private Dictionary<string, bool> bools = new Dictionary<string, bool>();
        private Dictionary<string, int> ints = new Dictionary<string, int>();

        public void Save(string key, string value)
        {
            strings[key] = value;
        }

        public string Get(string key, string defaultValue)
        {
            return strings.ContainsKey(key) ? strings[key] : defaultValue;
        }

        public void Save(string key, bool value)
        {
            bools[key] = value;
        }

        public bool Get(string key, bool defaultValue)
        {
            return bools.ContainsKey(key) ? bools[key] : defaultValue;
        }

        public void Save(string key, int value)
        {
            ints[key] = value;
        }

        public int Get(string key, int defaultValue)
        {
            return ints.ContainsKey(key) ? ints[key] : defaultValue;
        }
    }
}
