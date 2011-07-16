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
        #region Constructors

        public WidgetsScreen (IntPtr handle) : base (handle)
        {
        }

        [Export ("initWithCoder:")]
        public WidgetsScreen (NSCoder coder) : base (coder)
        {
        }

        public WidgetsScreen () : base ("WidgetsScreen", null)
        {
        }

        #endregion
        
        private const int SCREEN_WIDTH = 320;
        
        private SmeedeeApp app = SmeedeeApp.Instance;
		private IEnumerable<WidgetModel> EnabledWidgetModels { 
			get 
			{
	            var persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
				var enabledSettings = persistence.Get<Dictionary<string, bool>>("EnabledWidgets", null);
				return app.AvailableWidgets
					.Where(w => !enabledSettings.ContainsKey(w.Type.Name) || enabledSettings[w.Type.Name]);
			}}
		
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			titleLabel.StyleAsHeadline();
			subTitleLabel.StyleAsDescription();
            AttachScrollEvent();
            SetTitleLabels(0);
        }
				        
        private void AddWidgetsToScreen()
        {
            var widgets = GetEnabledWidgets();
            var count = widgets.Count();
            var scrollViewWidth = SCREEN_WIDTH * count;
            
            scrollView.Frame.Width = scrollViewWidth;
            scrollView.ContentSize = new SizeF(scrollViewWidth, SCREEN_WIDTH);
            
            for (int i = 0; i < count; i++)
            {
                var widget = widgets.ElementAt(i).View;
                
                var frame = scrollView.Frame;
                frame.Location = new PointF(SCREEN_WIDTH * i, 0);
                widget.Frame = frame;
                
                scrollView.AddSubview(widget);
            }
            
            pageControl.Pages = count;
        }
		
		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			EmptyScrollView();
            AddWidgetsToScreen();
			SetTitleLabels(CurrentPageIndex());
		}
        
		private void EmptyScrollView() {
			foreach (var view in scrollView.Subviews) 
				view.RemoveFromSuperview();
		}
		
        private IEnumerable<UIViewController> GetEnabledWidgets()
        {
            foreach (var widget in EnabledWidgetModels)
            {
            	var widgetInstance = Activator.CreateInstance(widget.Type) as UIViewController;
            	yield return widgetInstance;
            }
        }
        
        private void AttachScrollEvent()
        {
            scrollView.Scrolled += ScrollViewScrolled;
        }
        
		private int CurrentPageIndex()
        {
            var index = (int)Math.Floor((scrollView.ContentOffset.X - scrollView.Frame.Width / 2) / scrollView.Frame.Width) + 1;
            var max = (EnabledWidgetModels.Count() - 1);
            
            if (index < 0) return 0;
            if (index > max) return max;
            
            return index;
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
			if (EnabledWidgetModels.Count() == 0) {
				titleLabel.Text = "No enabled widgets";
	            subTitleLabel.Text = "Press configuration to enable";
			} else {
	            var currentWidget = EnabledWidgetModels.ElementAt(widgetIndex);			
	            var attribute = (WidgetAttribute)currentWidget.Type.GetCustomAttributes(typeof(WidgetAttribute), true).First();
	            titleLabel.Text = attribute.Name;
	            subTitleLabel.Text = attribute.StaticDescription;
			}
        }
        
        private void SetCurrentPage(int page)
        {
            pageControl.CurrentPage = page;
        }
    }
}
