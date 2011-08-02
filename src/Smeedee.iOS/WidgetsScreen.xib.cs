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
		
		private static UIActivityIndicatorView spinner = new UIActivityIndicatorView(UIActivityIndicatorViewStyle.Gray);
		
		private static int loadingCounter = 0;
		
		public static void StartLoading()
		{
			Console.WriteLine("show loading animation");
			loadingCounter++;
			spinner.Hidden = false;
			spinner.StartAnimating();
			UIApplication.SharedApplication.NetworkActivityIndicatorVisible = true;
		}
		
		public static void StopLoading()
		{
			Console.WriteLine("hide loading animation");
			if (loadingCounter > 0)
				loadingCounter--;
			if (loadingCounter == 0) 
			{
				UIApplication.SharedApplication.NetworkActivityIndicatorVisible = false;
				spinner.StopAnimating();
				spinner.Hidden = true;
			}
		}
		
		private IList<IWidget> widgets;
		
        public WidgetsScreen (IntPtr handle) : base (handle)
        {
			widgets = new List<IWidget>();
			spinner.Center = new PointF(320/2, 460/2);
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            scrollView.Scrolled += ScrollViewScrolled;
			refresh.Clicked += delegate {
				widgets.ElementAt(CurrentPageIndex()).Refresh();
			};
			pageControl.HidesForSinglePage = true;
			View.AddSubview(spinner);
			spinner.Hidden = true;
        }
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			
			EmptyScrollView();
			RemoveToolbarItem();
            InstantiateEnabledWidgets();
			AddWidgetsToScrollView();
			
			SetTitleLabels(CurrentPageIndex());
		}
        
		private void EmptyScrollView() {
			foreach (var view in scrollView.Subviews) 
				view.RemoveFromSuperview();
		}
		
		private void InstantiateEnabledWidgets()
		{
			widgets.Clear();
			foreach (var widgetModel in SmeedeeApp.Instance.EnabledWidgets) {
				var instance = Activator.CreateInstance(widgetModel.Type);
				widgets.Add(instance as IWidget);
			}
		}
		
        private void AddWidgetsToScrollView()
        {
            var count = widgets.Count();
            var scrollViewWidth = SCREEN_WIDTH * count;
            
            scrollView.Frame.Width = scrollViewWidth;
            scrollView.ContentSize = new SizeF(scrollViewWidth, SCREEN_WIDTH);
            
            for (int i = 0; i < count; i++)
            {
                var widget = (widgets.ElementAt(i) as UIViewController).View;
                
                var frame = scrollView.Frame;
                frame.Location = new PointF(SCREEN_WIDTH * i, 0);
                widget.Frame = frame;
                
                scrollView.AddSubview(widget);
            }
			
            pageControl.Pages = count;
            SetPageControlIndex(CurrentPageIndex());
        }
		
        private void ScrollViewScrolled(object sender, EventArgs e)
        {
			var pageIndex = CurrentPageIndex();
            if (pageControl.CurrentPage != pageIndex)
            {
                SetTitleLabels(pageIndex);
                SetPageControlIndex(pageIndex);
            }
        }
		
        private void SetTitleLabels(int widgetIndex)
        {
			if (widgets.Count() == 0) 
			{
				titleLabel.Text = "No enabled widgets";
			} 
			else 
			{
				var currentWidget = widgets.ElementAt(CurrentPageIndex());
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
			if (widgets.Count == 0)
				return 0;
            
			var index = (int)Math.Floor((scrollView.ContentOffset.X - scrollView.Frame.Width / 2) / scrollView.Frame.Width) + 1;
            var max = (widgets.Count() - 1);
            
            if (index < 0) return 0;
            if (index > max) return max;
            
            return index;
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
}
