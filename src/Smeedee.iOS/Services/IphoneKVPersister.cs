using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public class IphoneKVPersister : IPersistenceService
	{
        public void Save(string key, string val) {
			NSUserDefaults.StandardUserDefaults.SetString(val, key);
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}
		
        public string Get(string key, string defaultValue) {
			return NSUserDefaults.StandardUserDefaults.StringForKey(key) ?? defaultValue;
		}
		
		public void Save(string key, bool val) {
			NSUserDefaults.StandardUserDefaults.SetBool(val, key);
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}
		
        public bool Get(string key, bool defaultValue) {
			return NSUserDefaults.StandardUserDefaults.BoolForKey(key);
		}
	}
}

