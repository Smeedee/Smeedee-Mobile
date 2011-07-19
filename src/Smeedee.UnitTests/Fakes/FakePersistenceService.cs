using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.UnitTests.Fakes
{
    public class FakePersistenceService : IPersistenceService
    {
        public int SaveCalls, GetCalls;
        private Dictionary<string, object> cache = new Dictionary<string, object>();
        public void Save(string key, object value)
        {
            SaveCalls += 1;
            cache[key] = value;
        }

        public T Get<T>(string key, T defaultObject)
        {
            GetCalls += 1;
            return cache.ContainsKey(key) ? (T)cache[key] : defaultObject;
        }
    }
}
