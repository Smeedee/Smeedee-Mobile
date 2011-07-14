
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	[Widget("Working days left", StaticDescription = "Actual working days left of project")]
	public partial class WorkingDaysLeftWidget : UIViewController, IWidget
	{
        private SmeedeeApp app = SmeedeeApp.Instance;
        private IModelService<WorkingDaysLeft> service;
		private IBackgroundWorker bgWorker;
		private WorkingDaysLeft model;
		
		public WorkingDaysLeftWidget () : base("WorkingDaysLeftWidget", null)
		{
            service = app.ServiceLocator.Get<IModelService<WorkingDaysLeft>>();
            bgWorker = app.ServiceLocator.Get<IBackgroundWorker>();
		}
		
		public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			Refresh();
        }
		
		private void GetData() 
		{
			model = service.Get();
		}
		
		private void UpdateUI() 
		{
			daysLabel.Text = model.DaysLeft.ToString();
			overtimeLabel.Hidden = !model.IsOnOvertime;
		}
		
		public void Refresh() 
		{
			bgWorker.Invoke(() => {
				GetData();
				InvokeOnMainThread(UpdateUI);
			});
		}
		
		public string GetDynamicDescription() 
		{
			if (model != null) {
				return string.Format("Actual working days left until {0}", model.UntillDate.ToLongDateString());
			}
			return "Static";
		}
		
		
	}
}

