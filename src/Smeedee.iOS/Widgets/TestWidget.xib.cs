using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iOS
{
    [Widget("Foo Widget")]
    public partial class TestWidget : UIViewController, IWidget
    {
        public TestWidget() : base("TestWidget", null)
        {
        }
        
        public void Refresh()
        {
        }
		
		public string GetDynamicDescription() {
			return "";	
		}
    }
}

