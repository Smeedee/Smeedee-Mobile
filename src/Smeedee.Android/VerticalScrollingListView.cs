using System;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android
{
    public class VerticalScrollingListView : ListView
    {
        public VerticalScrollingListView(IntPtr doNotUse) : base(doNotUse)
        {
        }

        public VerticalScrollingListView(Context context) : base(context)
        {
        }

        public VerticalScrollingListView(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public VerticalScrollingListView(Context context, IAttributeSet attrs, int defStyle) : base(context, attrs, defStyle)
        {
        }

        public override bool OnTouchEvent(MotionEvent e)
        {
            base.OnTouchEvent(e);
            return false;
        }
    }
}