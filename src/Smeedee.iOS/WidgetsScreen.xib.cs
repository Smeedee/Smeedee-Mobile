using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.iOS.Lib;

namespace Smeedee.iOS
{
    public partial class WidgetsScreen : UIViewController
    {
		// Solving a race condition when the view is appearing
		private volatile bool appearing = false;
		
		private WidgetModel[] models;
		private IWidget[] widgets;
		private IWidget[] displayedWidgets;
		
		private UILabel title;
		
        public WidgetsScreen (IntPtr handle) : base (handle) { }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			View.AddSubview(LoadingIndicator.Instance);
			
			if (Platform.Name == Device.Ipad)
			{
				title = centeredTitle;
				title.Hidden = false;
				titleLabel.Hidden = true;
			}
			else
			{
				title = titleLabel;
			}
			
			pageControl.HidesForSinglePage = true;
			titleLabel.StyleAsWidgetHeadline();
			
            scrollView.Scrolled += ScrollViewScrolled;
			refresh.Clicked += delegate {
				if (displayedWidgets.Length > 0)
					displayedWidgets[CurrentPageIndex()].Refresh();
			};
			
			models = SmeedeeApp.Instance.AvailableWidgets.ToArray();
			widgets = new IWidget[models.Count()];
			
			for (int i = 0; i < models.Count(); ++i) {
				var instance = Activator.CreateInstance(models[i].Type);
				widgets[i] = (instance as IWidget);
			}
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
            var count = (from m in models where m.Enabled select m).Count();
            var scrollViewWidth = Platform.ScreenWidth * count;
			
			displayedWidgets = new IWidget[count];
            
            scrollView.Frame.Width = scrollViewWidth;
            scrollView.ContentSize = new SizeF(scrollViewWidth, Platform.ScreenWidth);
            
			if (count == 0)
			{
				var view = new UIImageView(UIImage.FromFile("images/logo.png"));
				view.Frame = new RectangleF((Platform.ScreenWidth - 61) / 2.0f, 140, 61, 61);
				this.scrollView.AddSubview(view);
			}
			else
			{
				int scrollViewIndex = 0;
				for (int i = 0; i < models.Count(); ++i)
				{
					if (models[i].Enabled)
					{
						displayedWidgets[scrollViewIndex] = widgets[i];
						var widgetView = (widgets[i] as UIViewController).View;
						
		                var frame = scrollView.Frame;
		                frame.Location = new PointF(Platform.ScreenWidth * scrollViewIndex, 0);
		                widgetView.Frame = frame;
		                
		                scrollView.AddSubview(widgetView);
						(widgets[i] as UIViewController).ViewWillAppear(true);
						
						scrollViewIndex++;
					}
				}
			}
			
            pageControl.Pages = count;
        	pageControl.CurrentPage = CurrentPageIndex();
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
            	pageControl.CurrentPage = pageIndex;
            }
        }
		
        private void SetTitleLabels(int widgetIndex)
        {
			
			if (displayedWidgets.Count() == 0) 
			{
				title.Text = "No enabled widgets";
			} 
			else 
			{
				var currentWidget = displayedWidgets[CurrentPageIndex()];
	            var attribute = currentWidget.GetType().GetCustomAttributes(typeof(WidgetAttribute), true).First() as WidgetAttribute;
	            
				title.Text = attribute.Name;
				
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
		
		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			Console.WriteLine("Rotating");
			Platform.Orientation = toInterfaceOrientation;
			ResetView();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
    }
}
