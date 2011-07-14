using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("Working days left", StaticDescription = "Actual working days left until 20.07.2011")]
	public partial class WorkingDaysLeftWidget : UIViewController, IWidget
	{
		public WorkingDaysLeftWidget () : base("WorkingDaysLeftWidget", null)
		{
		}
	
        public void Refresh()
        {
        }
		
		public string GetDynamicDescription() 
		{
			return "";	
		}
        
        public override void ViewDidLoad ()
        {
            base.ViewDidLoad ();
        }
	}
}

