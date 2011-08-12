using System;
using System.Drawing;
using MonoTouch.CoreGraphics;
using MonoTouch.UIKit;
using MonoTouch.Foundation;

namespace Smeedee.iOS.Views
{
    [Register("DisclosureIndicatorView")]
    public class DisclosureIndicatorView : UIControl
    {
        private const float R = 4.5f;
        private const float PADDING_RIGHT = 3;
        
        public DisclosureIndicatorView() : base(new RectangleF(0, 0, 10, 20))
        {
            BackgroundColor = UIColor.Clear;
        }
        
        public override void Draw(RectangleF rect)
        {
            base.Draw(rect);
            
            var context = UIGraphics.GetCurrentContext();
            
            // (x,y) is the tip of the arrow
            float x = 10 - PADDING_RIGHT;
            float y = 10;
            
            context.MoveTo(x-R, y-R);
            context.AddLineToPoint(x, y);
            context.AddLineToPoint(x-R, y+R);
            
            context.SetLineCap(CGLineCap.Square);
            context.SetLineJoin(CGLineJoin.Miter);
            context.SetLineWidth(3f);
        
            if (Highlighted)
            {
                context.SetRGBStrokeColor(0f, 0f, 0f, 0.6f);
            }
            else
            {
                context.SetRGBStrokeColor(255f, 255f, 255f, 0.6f);
            }
        
            context.StrokePath();
        }
        
        public override bool Highlighted
        {
            get
            {
                return base.Highlighted;
            }
            set
            {
                base.Highlighted = value;
                SetNeedsDisplay();
            }
        }
    }
}
