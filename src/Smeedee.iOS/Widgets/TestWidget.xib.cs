using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("TestWidget", 123, DescriptionStatic = "sadsa")]
    public partial class TestWidget : UIViewController, IWidget
    {
        #region Constructors

        public TestWidget(IntPtr handle) : base(handle)
        {
        }

        [Export("initWithCoder:")]
        public TestWidget(NSCoder coder) : base(coder)
        {
        }

        public TestWidget() : base("TestWidget", null)
        {
        }

        #endregion
        
        public void Refresh()
        {
        }
    }
}

