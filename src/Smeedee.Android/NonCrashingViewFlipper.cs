using System;

using Android.Content;
using Android.Util;
using Android.Widget;

namespace Smeedee.Android
{
    public class NonCrashingViewFlipper : ViewFlipper
    {
        public NonCrashingViewFlipper(Context context, IAttributeSet attrs) : base(context, attrs) { }

        protected override void OnDetachedFromWindow()
        {
            try
            {
                base.OnDetachedFromWindow();
            }
            catch (Exception e)
            {
                Log.Debug("TT","NonCrashingViewFlipper Stopped a viewflipper crash");
                base.StopFlipping();
            }
        }
    }
}