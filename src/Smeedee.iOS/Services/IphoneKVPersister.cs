using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public class IphoneKVPersister : IMobileKVPersister
	{
        public void Save(string key, string val) {
			NSUserDefaults.StandardUserDefaults.SetString(val, key);
			NSUserDefaults.StandardUserDefaults.Synchronize();
		}
		
        public string Get(string key) {
			return NSUserDefaults.StandardUserDefaults.StringForKey(key);
		}
	}
}

