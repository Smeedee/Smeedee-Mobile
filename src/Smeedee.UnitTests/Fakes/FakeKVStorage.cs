using System;
using System.Collections.Generic;

namespace Smeedee.UnitTests.Fakes
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

        public void Save(string key, bool value)
        {
            throw new NotImplementedException();
        }

        public bool Get(string key, bool defaultValue)
        {
            throw new NotImplementedException();
        }

        public void Save(string key, int value)
        {
            throw new NotImplementedException();
        }

        public int Get(string key, int defaultValue)
        {
            throw new NotImplementedException();
        }
    }
}