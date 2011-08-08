using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
	internal class LoadingIndicator : UIView
	{
		private int loadingCounter = 0;
		
		private UIActivityIndicatorView spinner;
		private UILabel label;
		
		private const int ScreenWidth = 320;
		private const int ScreenHeight = 400;
		private const int Padding = 10;
		private const int TextWidth = 65;
		private const int SpinnerSize = 20;
		private const int SeparateWidth = 5;
		private const int Width = Padding + SpinnerSize + SeparateWidth + TextWidth + Padding;
		private const int Height = Padding + SpinnerSize + Padding;
		
		// Singleton
		public readonly static LoadingIndicator Instance = new LoadingIndicator();
		
		private LoadingIndicator() : base()
		{
			Frame = new RectangleF((ScreenWidth - Width) / 2, ScreenHeight / 2, Width, Height);
			BackgroundColor = UIColor.FromWhiteAlpha(0.4f, 0.4f);
			
			spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray) {
				Frame = new RectangleF(Padding, Padding, SpinnerSize, SpinnerSize)
			};
			
			label = new UILabel() {
				Frame = new RectangleF(Padding + SpinnerSize + SeparateWidth, Padding, TextWidth, SpinnerSize),
				Text = "Loading...",
				TextColor = StyleExtensions.lightGrayText,
				BackgroundColor = StyleExtensions.transparent,
				AdjustsFontSizeToFitWidth = true
			};
			
			AddSubview(label);
			AddSubview(spinner);
			
			StopLoading();
		}
		
		public void StartLoading()
		{
			Console.WriteLine("show loading animation");
			loadingCounter++;
			Hidden = false;
			spinner.StartAnimating();
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}
		
		public void StopLoading()
		{
			Console.WriteLine("hide loading animation");
			if (loadingCounter > 0)
				loadingCounter--;
			if (loadingCounter == 0) 
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				spinner.StopAnimating();
				Hidden = true;
			}
		}
	}
}

