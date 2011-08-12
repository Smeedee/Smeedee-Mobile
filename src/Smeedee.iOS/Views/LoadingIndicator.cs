using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS.Views
{
	internal class LoadingIndicator : UIView
	{
		private ILog logger = SmeedeeApp.Instance.ServiceLocator.Get<ILog>();
		
		// Counting up and down when different objects call start/stop loading
		// Only hide view when counter reaches zero. Needs thread-safety.
		private int loadingCounter = 0;
		
		private UIActivityIndicatorView spinner;
		private UILabel label;
		
		// Singleton
		public readonly static LoadingIndicator Instance = new LoadingIndicator();
		
		private LoadingIndicator() : base()
		{
			int ScreenWidth = Platform.ScreenWidth;
			int ScreenHeight = Platform.ScreenHeight - 80;
			const int Padding = 10;
			const int TextWidth = 65;
			const int SpinnerSize = 20;
			const int SeparateWidth = 5;
			const int Width = Padding + SpinnerSize + SeparateWidth + TextWidth + Padding;
			const int Height = Padding + SpinnerSize + Padding;
		
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
			
			Hidden = true;
		}
		
		public void StartLoading()
		{
			lock (this)
			{
				loadingCounter++;
				logger.Log("Show loading animation", loadingCounter.ToString());
				Hidden = false;
				spinner.StartAnimating();
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
			}
		}
		
		public void StopLoading()
		{
			lock (this)
			{
				logger.Log("Hide loading animation", loadingCounter.ToString());
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
}

