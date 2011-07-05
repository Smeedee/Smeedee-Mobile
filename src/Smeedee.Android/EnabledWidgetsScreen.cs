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
    [Activity(Label = "EnabledWidgetsScreen", Theme = "@android:style/Theme.NoTitleBar")]
    public class EnabledWidgetsScreen : ListActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            List<string> widgetsAsStrings = new List<string>();
            widgetsAsStrings.Add("Widget1");
            widgetsAsStrings.Add("Widget2");
            widgetsAsStrings.Add("Widget3");

            ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.EnabledWidgetsListLayout, widgetsAsStrings);

            ListView.ItemClick += delegate(object sender, ItemEventArgs args)
            {
                // When clicked, show a toast with the TextView text  
                Toast.MakeText(Application, ((TextView)args.View).Text, ToastLength.Short).Show();
            }; 
        }
    }
}