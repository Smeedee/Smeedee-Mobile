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
	[MonoTouch.Foundation.Register("WidgetsScreen")]
	public partial class WidgetsScreen {
		
		private MonoTouch.UIKit.UIView __mt_view;
		
		private MonoTouch.UIKit.UIScrollView __mt_scrollView;
		
		private MonoTouch.UIKit.UIPageControl __mt_pageControl;
		
		private MonoTouch.UIKit.UILabel __mt_subTitleLabel;
		
		private MonoTouch.UIKit.UILabel __mt_titleLabel;
		
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
		
		[MonoTouch.Foundation.Connect("subTitleLabel")]
		private MonoTouch.UIKit.UILabel subTitleLabel {
			get {
				this.__mt_subTitleLabel = ((MonoTouch.UIKit.UILabel)(this.GetNativeField("subTitleLabel")));
				return this.__mt_subTitleLabel;
			}
			set {
				this.__mt_subTitleLabel = value;
				this.SetNativeField("subTitleLabel", value);
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
	}
}
