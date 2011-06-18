using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iPhone
{
	public partial class WidgetsScreen : UINavigationController
	{
		#region Constructors

		public WidgetsScreen(IntPtr handle) : base(handle)
		{
		}

		[Export ("initWithCoder:")]
		public WidgetsScreen(NSCoder coder) : base(coder)
		{
		}

		public WidgetsScreen() : base("WidgetsScreen", null)
		{
		}

		#endregion
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			var topCommitersScreen = new TopCommitersScreen();
			PushViewController(topCommitersScreen, false);
		}
	}
}
