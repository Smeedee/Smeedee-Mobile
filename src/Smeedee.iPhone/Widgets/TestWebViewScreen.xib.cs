using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Smeedee.Model;

namespace Smeedee.iPhone
{
    [Widget("Test Web Widget", 2, DescriptionStatic = "Testing a web widget")]
    public partial class TestWebViewScreen : UIViewController, IWidget
    {
        public TestWebViewScreen() : base("TestWebViewScreen", null)
        {
        }
        
        #region IWidget members
        
        public void Refresh()
        {
        }

        #endregion
        
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            
            var button = UIButton.FromType(UIButtonType.RoundedRect);
            button.Frame = new RectangleF(0, 0, 70, 35);
            button.SetTitle("Go", UIControlState.Normal);
            button.TouchUpInside += Button_Tapped;
            Add(button);
            
            var html = File.ReadAllText("Widgets/Html/Test.html");
            html = html.Replace("$content$",
                                "");
            webView.LoadHtmlString(html, new NSUrl(NSBundle.MainBundle.BundlePath + "/Widgets/Html", true));
        }

        private void Button_Tapped(object sender, EventArgs e)
        {
            webView.EvaluateJavascript("changeText('The text was changed by C# calling a JavaScript function!');");
        }
    }
}
