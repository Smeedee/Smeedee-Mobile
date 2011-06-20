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
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			CreatePanels();
		}
		
		private void CreatePanels()
		{
			scrollView.Scrolled += ScrollViewScrolled;
			
			int count = 4;
			var scrollFrame = scrollView.Frame;
			scrollFrame.Width = scrollFrame.Width * count;
			scrollView.ContentSize = new SizeF(1280, 320);
			// 320, 1280
			Console.WriteLine("Size: {0} / {1}", scrollView.ContentSize.Height, scrollView.ContentSize.Width);
			
			for (int i = 0; i < count; i++)
			{
				var label = new UILabel();
				label.TextColor = UIColor.White;
				label.TextAlignment = UITextAlignment.Center;
				label.Text = i.ToString();
				label.BackgroundColor = UIColor.Black;
				label.UserInteractionEnabled = false;
				
				var frame = scrollView.Frame;
				var location = new PointF();
				location.X = frame.Width * i;
				
				frame.Location = location;
				label.Frame = frame;
				Console.WriteLine("Label: {0} / {1}", frame.Height, frame.Width);
				
				scrollView.AddSubview(label);
			}
			
			var tc = new TopCommitersScreen();
			scrollView.AddSubview(tc.View);
			
			pageControl.Pages = count + 1;
		}
		
		private void ScrollViewScrolled(object sender, EventArgs e)
		{
			double page = Math.Floor((scrollView.ContentOffset.X - scrollView.Frame.Width / 2) / scrollView.Frame.Width) + 1;

			pageControl.CurrentPage = (int)page;
		}
	}
}
