using System;
using System.Collections.Generic;
using System.Text;
using Android.Content;
using Smeedee.Services;

namespace Smeedee.Android.Services
{
    class AndroidKVPersister : IMobileKVPersister
    {
        private readonly ISharedPreferences preferences;
        public AndroidKVPersister(Context context)
        {
            preferences = context.GetSharedPreferences("SmeedeePreferences", FileCreationMode.Private);
        }

        public void Save(string key, string value)
        {
            var editor = preferences.Edit();
            editor.PutString(key, value);
            editor.Commit();
        }

        public string Get(string key)
        {
            return preferences.GetString(key, null);
        }
    }
     

}
