﻿using System;
using System.Collections.Generic;
using System.IO.IsolatedStorage;

namespace Smeedee.WP7.Services
{
    public class WpPersister : IPersistenceService
    {
        private static IsolatedStorageSettings GetSettings()
        {
            return IsolatedStorageSettings.ApplicationSettings;
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

        private static void SaveObject(string key, object value)
        {
            var settings = GetSettings();
            if (settings.Contains(key))
                settings[key] = value;
            else
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

        private static T GetObject<T>(string key, T defaultValue)
        {
            var settings = GetSettings();
            if (!settings.Contains(key)) return defaultValue;
            try
            {
                return (T)settings[key];
            } catch (InvalidCastException)
            {
                return defaultValue;
            }
        }
    }
}
