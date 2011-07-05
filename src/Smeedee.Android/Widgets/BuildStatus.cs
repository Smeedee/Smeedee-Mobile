using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Widgets
{
    public class BuildStatus : LinearLayout, IWidget
    {
        public BuildStatus(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        public BuildStatus(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            var text = new TextView(Context);
            text.SetHeight(500);
            text.SetWidth(300);
            text.SetText(Resource.String.TestWidgetText);
            AddView(text);
        }
    }
}