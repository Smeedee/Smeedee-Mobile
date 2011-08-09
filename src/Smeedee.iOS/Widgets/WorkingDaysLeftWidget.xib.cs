using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;
using Smeedee.iOS.Lib;

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
			daysLabel.TextColor = StyleExtensions.smeedeeOrange;
			Refresh();
        }
		
		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			InvokeOnMainThread(UpdateUI);
		}
		
		private void UpdateUI() 
		{
			if (model.LoadError)
			{
				ToggleHidden(true);
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
			throw new NotImplementedException();
		}
		
        public event EventHandler DescriptionChanged;
        public void OnDescriptionChanged(EventArgs args)
        {
            if (DescriptionChanged != null)
                DescriptionChanged(this, args);
        }
	}
}

