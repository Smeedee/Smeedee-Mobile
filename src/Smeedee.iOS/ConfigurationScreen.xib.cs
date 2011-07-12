using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iOS
{
    public partial class ConfigurationScreen : UIViewController
    {
        #region Constructors

        // The IntPtr and initWithCoder constructors are required for items that need 
        // to be able to be created from a xib rather than from managed code

        public ConfigurationScreen (IntPtr handle) : base (handle)
        {
            Initialize ();
        }

        [Export ("initWithCoder:")]
        public ConfigurationScreen (NSCoder coder) : base (coder)
        {
            Initialize ();
        }

        public ConfigurationScreen () : base ("ConfigurationScreen", null)
        {
            Initialize ();
        }

        void Initialize ()
        {
        }

        #endregion
    }
}

