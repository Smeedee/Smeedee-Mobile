using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Widgets
{
    public class TestWidget : LinearLayout, IWidget
    {
        public TestWidget(Context context) : base(context)
        {
            Initialize();
        }

        public TestWidget(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            var text = new TextView(Context);
            text.SetHeight(500);
            text.SetWidth(400);
            text.SetText(Resource.String.TestWidgetText);
            text.SetTextColor(Color.White);
            text.SetBackgroundColor(Color.AliceBlue);
            AddView(text);
        }
    }
}