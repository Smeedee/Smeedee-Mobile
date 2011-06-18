using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iPhone
{
	public partial class ConfigurationScreen : UIViewController
	{
		#region Constructors

		public ConfigurationScreen(IntPtr handle) : base(handle)
		{
		}

		[Export ("initWithCoder:")]
		public ConfigurationScreen(NSCoder coder) : base(coder)
		{
		}

		public ConfigurationScreen() : base("ConfigurationScreen", null)
		{
		}
		
		#endregion
		
		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			
			// Write your custom logic here
		}
	}
}
