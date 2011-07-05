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
    public class LoginView : LinearLayout, IWidget
    {
        public LoginView(Context context)
            : base(context)
        {
            Initialize();
        }

        public LoginView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            Initialize();
        }

        private void Initialize()
        {
            var text = new TextView(Context);
            text.SetHeight(500);
            text.SetWidth(200);
            text.SetText(Resource.String.FooWidgetText);
            AddView(text);
        }
    }
}