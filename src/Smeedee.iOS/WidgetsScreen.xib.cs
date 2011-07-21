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
		
		private IList<IWidget> widgets;
		
        public WidgetsScreen (IntPtr handle) : base (handle)
        {
			widgets = new List<IWidget>();
        }
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			titleLabel.StyleAsHeadline();
			subTitleLabel.StyleAsDescription();
            scrollView.Scrolled += ScrollViewScrolled;
        }
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			
			EmptyScrollView();
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
        }
		
        private void ScrollViewScrolled(object sender, EventArgs e)
        {
			var pageIndex = CurrentPageIndex();
            if (pageControl.CurrentPage != pageIndex)
            {
                SetTitleLabels(pageIndex);
                SetCurrentPage(pageIndex);
            }
        }
		
        private void SetTitleLabels(int widgetIndex)
        {
			if (widgets.Count() == 0) {
				titleLabel.Text = "No enabled widgets";
	            subTitleLabel.Text = "Press configuration to enable";
			} else {
				var currentWidget = widgets.ElementAt(CurrentPageIndex());
	            var attribute = (WidgetAttribute) currentWidget.GetType().GetCustomAttributes(typeof(WidgetAttribute), true).First();
	            
				titleLabel.Text = attribute.Name;
	            subTitleLabel.Text = attribute.StaticDescription;
			}
        }
		
		private int CurrentPageIndex()
        {
            var index = (int)Math.Floor((scrollView.ContentOffset.X - scrollView.Frame.Width / 2) / scrollView.Frame.Width) + 1;
            var max = (widgets.Count() - 1);
            
            if (index < 0) return 0;
            if (index > max) return max;
            
            return index;
		}
		
        private void SetCurrentPage(int page)
        {
            pageControl.CurrentPage = page;
        }
    }
}
