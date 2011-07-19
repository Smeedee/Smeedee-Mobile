using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace Smeedee.iPhone
{
    public partial class TestWebViewScreen : UIViewController, IWidget
    {
        #region Constructors

        public TestWebViewScreen(IntPtr handle) : base(handle)
        {
        }

        [Export ("initWithCoder:")]
        public TestWebViewScreen(NSCoder coder) : base(coder)
        {
        }

        public TestWebViewScreen() : base("TestWebViewScreen", null)
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
