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
        /*
        public TestWidget(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            Initialize();
        }
        */
        private void Initialize()
        {
            var text = new TextView(this.Context);
            text.SetHeight(50);
            text.SetWidth(100);
            text.SetText(Resource.String.TestWidgetText);
            AddView(text);
        }
        /*
        protected override void OnDraw(Canvas canvas)
        {
            base.OnDraw(canvas);
            canvas.DrawColor(Color.Blue);
        }*/
    }
}