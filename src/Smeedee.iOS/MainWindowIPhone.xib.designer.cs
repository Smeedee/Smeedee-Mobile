// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.1
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Smeedee.iOS {
	
	
	// Base type probably should be MonoTouch.Foundation.NSObject or subclass
	[MonoTouch.Foundation.Register("AppDelegateIPhone")]
	public partial class AppDelegateIPhone {
		
		private MonoTouch.UIKit.UIWindow __mt_window;
		
		private MonoTouch.UIKit.UITabBarController __mt_tabBar;
		
		private ServerConfigTableViewController __mt_login;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("window")]
		private MonoTouch.UIKit.UIWindow window {
			get {
				this.__mt_window = ((MonoTouch.UIKit.UIWindow)(this.GetNativeField("window")));
				return this.__mt_window;
			}
			set {
				this.__mt_window = value;
				this.SetNativeField("window", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("tabBar")]
		private MonoTouch.UIKit.UITabBarController tabBar {
			get {
				this.__mt_tabBar = ((MonoTouch.UIKit.UITabBarController)(this.GetNativeField("tabBar")));
				return this.__mt_tabBar;
			}
			set {
				this.__mt_tabBar = value;
				this.SetNativeField("tabBar", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("login")]
		private ServerConfigTableViewController login {
			get {
				this.__mt_login = ((ServerConfigTableViewController)(this.GetNativeField("login")));
				return this.__mt_login;
			}
			set {
				this.__mt_login = value;
				this.SetNativeField("login", value);
			}
		}
	}
}
