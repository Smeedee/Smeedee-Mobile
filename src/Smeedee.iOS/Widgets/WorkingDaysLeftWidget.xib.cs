
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	[Widget("Working days left", StaticDescription = "Actual working days left of project", SettingsType = typeof(WorkingDaysLeftConfigTableViewController))]
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
			if (model.LoadError)
			{
				daysLabel.Text = "0";
				overtimeLabel.Text = "Error loading data";
			}
			else
			{
				daysLabel.Text = model.DaysLeft.ToString();
				overtimeLabel.Hidden = !model.IsOnOvertime;
			}
		}
		
		public void Refresh() 
		{
			model.Load(() => InvokeOnMainThread(UpdateUI));
		}
		
		public string GetDynamicDescription() 
		{
			return (!model.LoadError) ? string.Format("Actual working days left until {0}", model.UntillDate.ToLongDateString()) : "Actual working days left of project";
		}
		
        public event EventHandler DescriptionChanged;
        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
	}
}

