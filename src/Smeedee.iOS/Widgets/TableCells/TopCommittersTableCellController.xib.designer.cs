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
	[MonoTouch.Foundation.Register("TopCommittersTableCellController")]
	public partial class TopCommittersTableCellController {
		
		private MonoTouch.UIKit.UIImageView __mt_imageView;
		
		private MonoTouch.UIKit.UILabel __mt_nameLabel;
		
		private MonoTouch.UIKit.UILabel __mt_commitLabel;
		
		private MonoTouch.UIKit.UITableViewCell __mt_cell;
		
		#pragma warning disable 0169
		[MonoTouch.Foundation.Connect("imageView")]
		private MonoTouch.UIKit.UIImageView imageView {
			get {
				this.__mt_imageView = ((MonoTouch.UIKit.UIImageView)(this.GetNativeField("imageView")));
				return this.__mt_imageView;
			}
			set {
				this.__mt_imageView = value;
				this.SetNativeField("imageView", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("nameLabel")]
		private MonoTouch.UIKit.UILabel nameLabel {
			get {
				this.__mt_nameLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("nameLabel")));
				return this.__mt_nameLabel;
			}
			set {
				this.__mt_nameLabel = value;
				this.SetNativeField("nameLabel", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("commitLabel")]
		private MonoTouch.UIKit.UILabel commitLabel {
			get {
				this.__mt_commitLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("commitLabel")));
				return this.__mt_commitLabel;
			}
			set {
				this.__mt_commitLabel = value;
				this.SetNativeField("commitLabel", value);
			}
		}
		
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
	}
}
