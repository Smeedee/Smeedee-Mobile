using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iPhone
{
    [Widget("Welcome Screen", 2, DescriptionStatic = "No widgets yet")]
    public partial class SmeedeeWelcomeScreen : UIViewController, IWidget
    {
        public SmeedeeWelcomeScreen() : base("SmeedeeWelcomeScreen", null)
        {
        }
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
        }
    }
}
