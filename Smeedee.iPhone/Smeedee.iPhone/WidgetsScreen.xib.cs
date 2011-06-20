using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iPhone
{
	public partial class WidgetsScreen : UIViewController
	{
		#region Constructors

		public WidgetsScreen(IntPtr handle) : base(handle)
		{
		}

		[Export ("initWithCoder:")]
		public WidgetsScreen(NSCoder coder) : base(coder)
		{
		}

		public WidgetsScreen() : base("WidgetsScreen", null)
		{
		}

		#endregion
		
		private const int SCREEN_WIDTH = 320;
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			AddWidgetsToScreen();
			AttachScrollEvent();
		}
		
		private void AddWidgetsToScreen()
		{
			var widgets = new UIViewController[] {
				new SmeedeeWelcomeScreen(),
				new TopCommitersScreen()
			};
			
			var count = widgets.Length;
			var scrollViewWidth = SCREEN_WIDTH * count;
			
			scrollView.Frame.Width = scrollViewWidth;
			scrollView.ContentSize = new SizeF(scrollViewWidth, SCREEN_WIDTH);
			
			for (int i = 0; i < count; i++)
			{
				var widget = widgets[i].View;
				
				var location = new PointF();
				location.X = SCREEN_WIDTH * i;
				
				var frame = scrollView.Frame;
				frame.Location = location;
				widget.Frame = frame;
				
				scrollView.AddSubview(widget);
			}
			
			pageControl.Pages = count;
		}
		
		private void AttachScrollEvent()
		{
			scrollView.Scrolled += ScrollViewScrolled;
		}
		
		private void ScrollViewScrolled(object sender, EventArgs e)
		{
			double page = Math.Floor((scrollView.ContentOffset.X - scrollView.Frame.Width / 2) / scrollView.Frame.Width) + 1;

			pageControl.CurrentPage = (int)page;
		}
	}
}
