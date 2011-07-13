using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("Bar Widget", 123, DescriptionStatic = "Sed ut perspiciatis unde omnis iste natus")]
    public partial class TestWidget2 : UIViewController, IWidget
    {
        public TestWidget2() : base("TestWidget2", null)
        {
        }
        
        public void Refresh()
        {
        }
    }
}
