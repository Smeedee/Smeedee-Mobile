// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 2.0.50727.1433
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace Smeedee.iPhone {
	
	
	// Base type probably should be MonoTouch.UIKit.UIViewController or subclass
	[MonoTouch.Foundation.Register("TopCommiterTableCell")]
	public partial class TopCommiterTableCell {
		
		private MonoTouch.UIKit.UITableViewCell __mt_cell;
		
		private MonoTouch.UIKit.UIImageView __mt_avatarImage;
		
		private MonoTouch.UIKit.UILabel __mt_commitsLabel;
		
		private MonoTouch.UIKit.UILabel __mt_userLabel;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("cell")]
		private MonoTouch.UIKit.UITableViewCell cell {
			get {
				this.__mt_cell = ((MonoTouch.UIKit.UITableViewCell)(this.GetNativeField("cell")));
				return this.__mt_cell;
			}
			set {
				this.__mt_cell = value;
				this.SetNativeField("cell", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("avatarImage")]
		private MonoTouch.UIKit.UIImageView avatarImage {
			get {
				this.__mt_avatarImage = ((MonoTouch.UIKit.UIImageView)(this.GetNativeField("avatarImage")));
				return this.__mt_avatarImage;
			}
			set {
				this.__mt_avatarImage = value;
				this.SetNativeField("avatarImage", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("commitsLabel")]
		private MonoTouch.UIKit.UILabel commitsLabel {
			get {
				this.__mt_commitsLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("commitsLabel")));
				return this.__mt_commitsLabel;
			}
			set {
				this.__mt_commitsLabel = value;
				this.SetNativeField("commitsLabel", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("userLabel")]
		private MonoTouch.UIKit.UILabel userLabel {
			get {
				this.__mt_userLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("userLabel")));
				return this.__mt_userLabel;
			}
			set {
				this.__mt_userLabel = value;
				this.SetNativeField("userLabel", value);
			}
		}
	}
}
