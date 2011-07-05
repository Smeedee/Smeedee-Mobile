using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android
{
    [Activity(Label = "EnabledWidgetsScreen")]
    public class EnabledWidgetsScreen : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var view = FindViewById(Resource.Id.EnabledWidgetsTextView);
            
            SetContentView(view);
        }
    }
}