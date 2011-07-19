using System;
using Android.Content;
using Android.Preferences;
using Java.Lang;
using Smeedee;

namespace Smeedee.Android.Services
{
    public class AndroidKVPersister : IPersistenceService
    {
        private readonly ISharedPreferences preferences;
        public AndroidKVPersister(Context context)
        {
            preferences = PreferenceManager.GetDefaultSharedPreferences(context);
        }

        public void Save(string key, string value)
        {
            var editor = preferences.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }


        public string Get(string key, string defaultValue)
        {
            try
            {
                return preferences.GetString(key, defaultValue);
            } catch (ClassCastException e)
            {
                return defaultValue;
            }
        }

        public void Save(string key, bool value)
        {
            var editor = preferences.Edit();
            editor.PutBoolean(key, value);
            editor.Commit();
        }

        public bool Get(string key, bool defaultValue)
        {
            try
            {
                return preferences.GetBoolean(key, defaultValue);
            }
            catch (ClassCastException e)
            {
                return defaultValue;
            }
        }
    }
     

}
