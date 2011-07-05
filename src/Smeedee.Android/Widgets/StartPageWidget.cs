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
    public class StartPageWidget : LinearLayout, IWidget
    {
        private Context context;

        public StartPageWidget(Context context) :
            base(context)
        {
            this.context = context;
            Initialize();
        }

        public StartPageWidget(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            this.context = context;
            Initialize();
        }

        private void Initialize()
        {
            var inflater = Context.GetSystemService(Context.LayoutInflaterService) as LayoutInflater;
            inflater.Inflate(Resource.Layout.StartPageWidgetLayout, this);
        }
    }
}