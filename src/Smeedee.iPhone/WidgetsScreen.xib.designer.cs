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
	[MonoTouch.Foundation.Register("WidgetsScreen")]
	public partial class WidgetsScreen {
		
		private MonoTouch.UIKit.UIView __mt_view;
		
		private MonoTouch.UIKit.UIPageControl __mt_pageControl;
		
		private MonoTouch.UIKit.UIScrollView __mt_scrollView;
		
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
	}
}
