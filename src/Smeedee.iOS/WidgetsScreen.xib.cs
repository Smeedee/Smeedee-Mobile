using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    public partial class WidgetsScreen : UIViewController
    {
        private const int SCREEN_WIDTH = 320;
		
		// Solving a race condition when the view is appearing
		private volatile bool appearing = false;
		
		private WidgetModel[] models;
		private IWidget[] widgets;
		private IWidget[] displayedWidgets;
		
        public WidgetsScreen (IntPtr handle) : base (handle) { }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			View.AddSubview(LoadingIndicator.Instance);
			
			pageControl.HidesForSinglePage = true;
			
            scrollView.Scrolled += ScrollViewScrolled;
			refresh.Clicked += delegate {
				displayedWidgets[CurrentPageIndex()].Refresh();
			};
			
			models = SmeedeeApp.Instance.AvailableWidgets.ToArray();
			widgets = new IWidget[models.Count()];
			
			for (int i = 0; i < models.Count(); ++i) {
				var instance = Activator.CreateInstance(models[i].Type);
				widgets[i] = (instance as IWidget);
			}
			
			ResetView();
        }
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			
			appearing = true;
			ResetView();
			appearing = false;
			
			SetTitleLabels(CurrentPageIndex());
		}
		
		private void ResetView()
		{
			RemoveToolbarItem();
			EmptyScrollView();
			AddWidgetsToScrollView();
		}
		
		private void EmptyScrollView() {
			foreach (var view in scrollView.Subviews) 
				view.RemoveFromSuperview();
		}
		
        private void AddWidgetsToScrollView()
        {
            var count = NumberOfEnabledWidgets();
            var scrollViewWidth = SCREEN_WIDTH * count;
			
			displayedWidgets = new IWidget[count];
            
            scrollView.Frame.Width = scrollViewWidth;
            scrollView.ContentSize = new SizeF(scrollViewWidth, SCREEN_WIDTH);
            
			int scrollViewIndex = 0;
			for (int i = 0; i < models.Count(); ++i)
			{
				if (models[i].Enabled)
				{
					displayedWidgets[scrollViewIndex] = widgets[i];
					var widgetView = (widgets[i] as UIViewController).View;
					
	                var frame = scrollView.Frame;
	                frame.Location = new PointF(SCREEN_WIDTH * scrollViewIndex, 0);
	                widgetView.Frame = frame;
	                
	                scrollView.AddSubview(widgetView);
					
					scrollViewIndex++;
				}
			}
            pageControl.Pages = count;
            SetPageControlIndex(CurrentPageIndex());
        }
		
		// Race condition between this and ViewWillAppear
		// Both are called simultanously when going from settings to widgets
        private void ScrollViewScrolled(object sender, EventArgs e)
        {
			if (appearing) return;
			var pageIndex = CurrentPageIndex();
            if (pageControl.CurrentPage != pageIndex)
            {
                SetTitleLabels(pageIndex);
                SetPageControlIndex(pageIndex);
            }
        }
		
        private void SetTitleLabels(int widgetIndex)
        {
			if (displayedWidgets.Count() == 0) 
			{
				titleLabel.Text = "No enabled widgets";
			} 
			else 
			{
				Console.WriteLine("Setting labels for index " + CurrentPageIndex());
				Console.WriteLine("Currently displaying " + displayedWidgets.Count() + " widgets");
				var currentWidget = displayedWidgets[CurrentPageIndex()];
	            var attribute = currentWidget.GetType().GetCustomAttributes(typeof(WidgetAttribute), true).First() as WidgetAttribute;
	            
				titleLabel.Text = attribute.Name;
				
				if (currentWidget is IToolbarControl) 
				{
					var item = (currentWidget as IToolbarControl).ToolbarConfigurationItem();
					AddToolbarItem(item);
				}
				else
				{
					RemoveToolbarItem();
				}
			}
        }
		
		private int CurrentPageIndex()
        {
			if (displayedWidgets.Count() == 0)
				return 0;
            
			var index = (int)Math.Floor((scrollView.ContentOffset.X - scrollView.Frame.Width / 2) / scrollView.Frame.Width) + 1;
            var max = (displayedWidgets.Count() - 1);
            
            if (index < 0) return 0;
            if (index > max) return max;
            
            return index;
		}
		
		private int NumberOfEnabledWidgets()
		{
			var count = 0;
			foreach (var m in models)
				if (m.Enabled) count++;
			return count;
		}
		
        private void SetPageControlIndex(int page)
        {
			Console.WriteLine("Setting page index to " + page);
            pageControl.CurrentPage = page;
        }
		
		// Inline configuration toolbar
		//
		private void AddToolbarItem(UIBarButtonItem item)
		{
			if (toolbar.Items.Count() == 3)
				toolbar.SetItems(new [] { toolbar.Items[0], item, toolbar.Items[2] }, true);
			else
				toolbar.SetItems(new [] { toolbar.Items[0], item, toolbar.Items[1] }, true);
		}
		
		private void RemoveToolbarItem()
		{
			if (toolbar.Items.Count() == 3)
				toolbar.SetItems(new [] { toolbar.Items[0], toolbar.Items[2] }, true);
		}
    }
	
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
