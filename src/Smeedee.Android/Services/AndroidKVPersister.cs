using Android.Content;
using Smeedee;

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
        public bool GetBoolean(string key)
        {
            return preferences.GetBoolean(key, false);
        }
    }
     

}
