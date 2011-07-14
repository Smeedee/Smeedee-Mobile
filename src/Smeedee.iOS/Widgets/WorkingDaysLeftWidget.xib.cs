
using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
	[Widget("Working days left", StaticDescription = "See which developer has committed the most code")]
	public partial class WorkingDaysLeftWidget : UIViewController, IWidget
	{
		public WorkingDaysLeftWidget () : base("WorkingDaysLeftWidget", null)
		{
			
		}
		
		public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
			daysLabel.Text = "20";
        }
		
		public void Refresh() 
		{
			
		}
		
		public string GetDynamicDescription() 
		{
			return "";
		}
		
		
	}
}

