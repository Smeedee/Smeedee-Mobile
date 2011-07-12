using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("TestWidget2", 123, DescriptionStatic = "TODO")]
    public partial class TestWidget2 : UIViewController, IWidget
    {
        #region Constructors

        public TestWidget2(IntPtr handle) : base(handle)
        {
        }

        [Export ("initWithCoder:")]
        public TestWidget2(NSCoder coder) : base(coder)
        {
        }

        public TestWidget2() : base("TestWidget2", null)
        {
        }

        #endregion
        
        public void Refresh()
        {
        }
    }
}
