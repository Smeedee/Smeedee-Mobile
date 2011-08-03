using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Smeedee.WP7.Services
{
    public class WpPersister : IPersistenceService
    {
        private IsolatedStorageSettings GetSettings()
        {
            return System.IO.IsolatedStorage.IsolatedStorageSettings.ApplicationSettings;
        }

        public void Save(string key, string value)
        {
            SaveObject(key, value);
        }

        public void Save(string key, int value)
        {
            SaveObject(key, value);
        }

        public void Save(string key, bool value)
        {
            SaveObject(key, value);
        }

        private void SaveObject(string key, object value)
        {
            var settings = GetSettings();
            settings.Add(key, value);
            settings.Save();
        }

        public string Get(string key, string defaultValue)
        {
            return GetObject(key, defaultValue);
        }


        public bool Get(string key, bool defaultValue)
        {
            return GetObject(key, defaultValue);
        }


        public int Get(string key, int defaultValue)
        {
            return GetObject(key, defaultValue);
        }

        private T GetObject<T>(string key, T defaultValue)
        {   
            try
            {
                return (T)GetSettings()[key];
            } catch (InvalidCastException)
            {
                return defaultValue;
            } catch (KeyNotFoundException)
            {
                return defaultValue;
            }
        }
    }
}
