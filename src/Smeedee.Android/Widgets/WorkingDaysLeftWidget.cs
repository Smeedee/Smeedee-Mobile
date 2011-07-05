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
    public class WorkingDaysLeftWidget : RelativeLayout/*, IWidget*/
    {
        public WorkingDaysLeftWidget(Context context)
            : base(context)
        {
            Initialize();
        }

        private void Initialize()
        {
        }
    }
}