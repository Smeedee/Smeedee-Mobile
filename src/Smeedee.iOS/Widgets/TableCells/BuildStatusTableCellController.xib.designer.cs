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
	[MonoTouch.Foundation.Register("BuildStatusTableCellController")]
	public partial class BuildStatusTableCellController {
		
		private MonoTouch.UIKit.UITableViewCell __mt_cell;
		
		private MonoTouch.UIKit.UILabel __mt_projectNameLabel;
		
		private MonoTouch.UIKit.UILabel __mt_usernameLabel;
		
		private MonoTouch.UIKit.UILabel __mt_lastBuildTimeLabel;
		
		private MonoTouch.UIKit.UIImageView __mt_image;
		
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
		
		[MonoTouch.Foundation.Connect("projectNameLabel")]
		private MonoTouch.UIKit.UILabel projectNameLabel {
			get {
				this.__mt_projectNameLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("projectNameLabel")));
				return this.__mt_projectNameLabel;
			}
			set {
				this.__mt_projectNameLabel = value;
				this.SetNativeField("projectNameLabel", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("usernameLabel")]
		private MonoTouch.UIKit.UILabel usernameLabel {
			get {
				this.__mt_usernameLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("usernameLabel")));
				return this.__mt_usernameLabel;
			}
			set {
				this.__mt_usernameLabel = value;
				this.SetNativeField("usernameLabel", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("lastBuildTimeLabel")]
		private MonoTouch.UIKit.UILabel lastBuildTimeLabel {
			get {
				this.__mt_lastBuildTimeLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("lastBuildTimeLabel")));
				return this.__mt_lastBuildTimeLabel;
			}
			set {
				this.__mt_lastBuildTimeLabel = value;
				this.SetNativeField("lastBuildTimeLabel", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("image")]
		private MonoTouch.UIKit.UIImageView image {
			get {
				this.__mt_image = ((MonoTouch.UIKit.UIImageView)(this.GetNativeField("image")));
				return this.__mt_image;
			}
			set {
				this.__mt_image = value;
				this.SetNativeField("image", value);
			}
		}
	}
}
