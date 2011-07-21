using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
	public partial class BuildStatusConfigTableViewController : UIViewController
	{
		#region Constructors

		// The IntPtr and initWithCoder constructors are required for items that need 
		// to be able to be created from a xib rather than from managed code

		public BuildStatusConfigTableViewController (IntPtr handle) : base (handle)
		{
			Initialize ();
		}

		[Export ("initWithCoder:")]
		public BuildStatusConfigTableViewController (NSCoder coder) : base (coder)
		{
			Initialize ();
		}

		public BuildStatusConfigTableViewController () : base ("BuildStatusConfigTableViewController", null)
		{
			Initialize ();
		}

		void Initialize ()
		{
		}

		#endregion
	}
}

