using Android.Content;
using Android.Util;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Widgets
{
    [WidgetAttribute("Latest Changesets", Resource.Drawable.Icon, IsEnabled = true)]
    public class LatestChangesetsWidget : RelativeLayout, IWidget
    {
        public LatestChangesetsWidget(Context context) :
            base(context)
        {
            Initialize();
        }

        public LatestChangesetsWidget(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {

        }
    }
}