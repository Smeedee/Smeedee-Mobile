using System.Collections.Generic;
using Smeedee;

namespace Smeedee.UnitTests.Services
{
    public class FakeKVStorage : IPersistenceService
    {
        public Dictionary<string, string> savedValues;
        public string retrievableContent { get; set; }

        public FakeKVStorage()
        {
            savedValues = new Dictionary<string, string>();
        }

        public void Save(string key, string value)
        {
            savedValues.Add(key, value);
        }
        
        public string Get(string key, string defaultValue)
        {
            if (string.IsNullOrEmpty(retrievableContent))
                return savedValues[key];
            return retrievableContent;
        }
    }
}