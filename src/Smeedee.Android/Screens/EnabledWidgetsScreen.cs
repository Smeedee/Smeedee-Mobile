using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.OS;
using Android.Widget;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "EnabledWidgetsScreen", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar")]
    public class EnabledWidgetsScreen : Activity
    {
        //private IWidgetModelsService service = SmeedeeApp.Instance.ServiceLocator.Get<WidgetModelsService>();


        public EnabledWidgetsScreen()
        {
            // service.GetAllWidgetModels and store them in a list
            // take out the data I need
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EnabledWidgetsScreen);

            var listView = FindViewById<ListView>(Resource.Id.EnabledWidgetsScreenBuildList);

            // create the grid item mapping
            var from = new[] { "WidgetTitle" };
            var to = new[] { Resource.Id.WidgetTitle };

            // prepare the list of all records
            IList<IDictionary<String, object>> fillMaps = new List<IDictionary<String, object>>();
            for (var i = 0; i < 10; i++)
            {
                IDictionary<String, object> map = new Dictionary<String, object>();
                map.Add("WidgetTitle", "Widget " + i);
                                                      
                fillMaps.Add(map);
            }
            // fill in the grid_item layout
            var adapter = new SimpleAdapter(this, fillMaps, Resource.Layout.EnabledWidgetsScreen_ListItem, from, to);
            listView.Adapter = adapter;
            
        }
    }
}