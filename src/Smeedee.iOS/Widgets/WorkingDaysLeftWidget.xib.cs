
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
		private WorkingDaysLeft model;
		
		public WorkingDaysLeftWidget () : base("WorkingDaysLeftWidget", null)
		{
            model = new WorkingDaysLeft();
		}
		
		public override void ViewDidLoad()
        {
            base.ViewDidLoad();
			daysLabel.StyleAsHeadline();
			Refresh();
        }
		
		private void UpdateUI() 
		{
			daysLabel.Text = model.DaysLeft.ToString();
			overtimeLabel.Hidden = !model.IsOnOvertime;
		}
		
		public void Refresh() 
		{
			model.Load(() => InvokeOnMainThread(UpdateUI));
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

