using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Smeedee.WP7.Services.Fakes
{
    public class FakePersister : IPersistenceService
    {
        private Dictionary<string, string> strings = new Dictionary<string, string>(); 
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
