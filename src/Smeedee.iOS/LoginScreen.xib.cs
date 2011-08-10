using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public partial class LoginScreen : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public LoginScreen (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		[Export ("initWithCoder:")]
		public LoginScreen (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		public LoginScreen () : base ("LoginScreen", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}

		#endregion
	
	
		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			Platform.Orientation = toInterfaceOrientation;
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation(UIInterfaceOrientation toInterfaceOrientation)
		{
			return true;
		}
	}
	
}

