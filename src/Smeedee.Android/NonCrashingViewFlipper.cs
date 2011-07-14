using System;
using Android.Content;
using Android.Util;
using Android.Widget;

namespace Smeedee.Android
{
    /**
     * Custom ViewFlipper that suppresses known issue that will crash the application when rotating screen.
     * 
     * See 
     *   http://code.google.com/p/android/issues/detail?id=6191
     * and
     *   http://stackoverflow.com/questions/3019606/why-does-keyboard-slide-crash-my-app/3026985#3026985
     * for issue report and information.
     */
    public class NonCrashingViewFlipper : ViewFlipper
    {
        public NonCrashingViewFlipper(Context context, IAttributeSet attrs) 
            : base(context, attrs) { }

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
        public int GetPreviousChildIndex()
        {
            if (DisplayedChild == ChildCount-1) 
                return 0;
            return DisplayedChild + 1;
        }

        public int GetNextChildIndex()
        {
            if (DisplayedChild == 0)
                return ChildCount - 1;
            return DisplayedChild - 1;
        }
    }
}