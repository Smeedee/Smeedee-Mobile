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
	[MonoTouch.Foundation.Register("WidgetsScreen")]
	public partial class WidgetsScreen {
		
		private MonoTouch.UIKit.UIScrollView __mt_scrollView;
		
		private MonoTouch.UIKit.UIPageControl __mt_pageControl;
		
		private MonoTouch.UIKit.UIToolbar __mt_toolbar;
		
		private MonoTouch.UIKit.UIView __mt_view;
		
		private MonoTouch.UIKit.UIBarButtonItem __mt_refresh;
		
		private MonoTouch.UIKit.UILabel __mt_titleLabel;
		
		private MonoTouch.UIKit.UILabel __mt_centeredTitle;
		
		#pragma warning disable 0169
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
		
		[MonoTouch.Foundation.Connect("pageControl")]
		private MonoTouch.UIKit.UIPageControl pageControl {
			get {
				this.__mt_pageControl = ((MonoTouch.UIKit.UIPageControl)(this.GetNativeField("pageControl")));
				return this.__mt_pageControl;
			}
			set {
				this.__mt_pageControl = value;
				this.SetNativeField("pageControl", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("toolbar")]
		private MonoTouch.UIKit.UIToolbar toolbar {
			get {
				this.__mt_toolbar = ((MonoTouch.UIKit.UIToolbar)(this.GetNativeField("toolbar")));
				return this.__mt_toolbar;
			}
			set {
				this.__mt_toolbar = value;
				this.SetNativeField("toolbar", value);
			}
		}
		
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
		
		[MonoTouch.Foundation.Connect("refresh")]
		private MonoTouch.UIKit.UIBarButtonItem refresh {
			get {
				this.__mt_refresh = ((MonoTouch.UIKit.UIBarButtonItem)(this.GetNativeField("refresh")));
				return this.__mt_refresh;
			}
			set {
				this.__mt_refresh = value;
				this.SetNativeField("refresh", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("titleLabel")]
		private MonoTouch.UIKit.UILabel titleLabel {
			get {
				this.__mt_titleLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("titleLabel")));
				return this.__mt_titleLabel;
			}
			set {
				this.__mt_titleLabel = value;
				this.SetNativeField("titleLabel", value);
			}
		}
		
		[MonoTouch.Foundation.Connect("centeredTitle")]
		private MonoTouch.UIKit.UILabel centeredTitle {
			get {
				this.__mt_centeredTitle = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("centeredTitle")));
				return this.__mt_centeredTitle;
			}
			set {
				this.__mt_centeredTitle = value;
				this.SetNativeField("centeredTitle", value);
			}
		}
	}
}
