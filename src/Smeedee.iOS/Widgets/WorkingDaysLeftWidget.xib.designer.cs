// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Smeedee.iOS {
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[MonoTouch.Foundation.Register("WorkingDaysLeftWidget")]
	public partial class WorkingDaysLeftWidget {
		
		private MonoTouch.UIKit.UIView __mt_view;
		
		private MonoTouch.UIKit.UILabel __mt_daysLabel;
		
		private MonoTouch.UIKit.UILabel __mt_overtimeLabel;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("view")]
		private MonoTouch.UIKit.UIView view {
			get {
				this.__mt_view = ((MonoTouch.UIKit.UIView)(this.GetNativeField("view")));
				return this.__mt_view;
			}
			set {
				this.__mt_view = value;
				this.SetNativeField("view", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("daysLabel")]
		private MonoTouch.UIKit.UILabel daysLabel {
			get {
				this.__mt_daysLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("daysLabel")));
				return this.__mt_daysLabel;
			}
			set {
				this.__mt_daysLabel = value;
				this.SetNativeField("daysLabel", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("overtimeLabel")]
		private MonoTouch.UIKit.UILabel overtimeLabel {
			get {
				this.__mt_overtimeLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("overtimeLabel")));
				return this.__mt_overtimeLabel;
			}
			set {
				this.__mt_overtimeLabel = value;
				this.SetNativeField("overtimeLabel", value);
			}
		}
	}
}
