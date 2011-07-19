using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.UnitTests.Fakes
{
    public class FakePersistenceService : IPersistenceService
    {
        public int SaveCalls, GetCalls;
        private Dictionary<string, string> cache = new Dictionary<string, string>();
        public void Save(string key, string value)
        {
            SaveCalls += 1;
            cache[key] = value;
        }

        public string Get(string key, string defaultObject)
        {
            GetCalls += 1;
            return cache.ContainsKey(key) ? cache[key] : defaultObject;
        }

        public void Save(string key, bool value)
        {
            SaveCalls += 1;
            cache[key] = value.ToString();
        }
        public bool Get(string key, bool defaultValue)
        {
            GetCalls += 1;
            return cache.ContainsKey(key) ? bool.Parse(cache[key]) : defaultValue;
        }
    }
}
