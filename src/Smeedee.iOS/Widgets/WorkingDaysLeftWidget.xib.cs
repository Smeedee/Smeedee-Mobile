
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
			ToggleHidden(true);
			Refresh();
        }
		
		private void UpdateUI() 
		{
			if (model.LoadError)
			{
				topLabel.Text = "Error loading data";
			}
			else
			{
				ToggleHidden(false);
				
				daysLabel.Text = model.DaysLeft.ToString();
				topLabel.Text = model.DaysLeftText;
				bottomLabel.Text = string.Format("until {0:dddd, MMMM d, yyyy}", model.UntillDate);
				bottomLabel.Hidden = model.IsOnOvertime;
			}
		}
		
		private void ToggleHidden(bool hidden)
		{
			daysLabel.Hidden = hidden;
			topLabel.Hidden = hidden;
			bottomLabel.Hidden = hidden;
		}
		
		public void Refresh() 
		{
			InvokeOnMainThread(LoadingIndicator.Instance.StartLoading);
			model.Load(() => {
				InvokeOnMainThread(UpdateUI);
				InvokeOnMainThread(LoadingIndicator.Instance.StopLoading);
			});
		}
		
		public string GetDynamicDescription() 
		{
			return (!model.LoadError) ? string.Format("Actual working days left until {0}", model.UntillDate.ToLongDateString()) : "Actual working days left of project";
		}
		
		public DateTime LastRefreshTime()
		{
			return DateTime.Now;	
		}
		
        public event EventHandler DescriptionChanged;
        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
	}
}

