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
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[MonoTouch.Foundation.Register("ServerConfigTableViewController")]
	public partial class ServerConfigTableViewController {
		
		private MonoTouch.UIKit.UITableView __mt_table;
		
		private MonoTouch.UIKit.UIScrollView __mt_scrollView;
		
		private MonoTouch.UIKit.UIScrollView __mt_view;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("table")]
		private MonoTouch.UIKit.UITableView table {
			get {
				this.__mt_table = ((MonoTouch.UIKit.UITableView)(this.GetNativeField("table")));
				return this.__mt_table;
			}
			set {
				this.__mt_table = value;
				this.SetNativeField("table", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("scrollView")]
		private MonoTouch.UIKit.UIScrollView scrollView {
			get {
				this.__mt_scrollView = ((MonoTouch.UIKit.UIScrollView)(this.GetNativeField("scrollView")));
				return this.__mt_scrollView;
			}
			set {
				this.__mt_scrollView = value;
				this.SetNativeField("scrollView", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("view")]
		private MonoTouch.UIKit.UIScrollView view {
			get {
				this.__mt_view = ((MonoTouch.UIKit.UIScrollView)(this.GetNativeField("view")));
				return this.__mt_view;
			}
			set {
				this.__mt_view = value;
				this.SetNativeField("view", value);
			}
		}
	}
}
