using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Enable or disable widgets", Theme = "@android:style/Theme")]
    public class EnabledWidgetsScreen : Activity
    {
        private readonly SmeedeeApp _app = SmeedeeApp.Instance;
        private ListView _listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EnabledWidgetsScreen);

            _listView = FindViewById<ListView>(Resource.Id.EnabledWidgetsScreenBuildList);

            var from = new[] { "WidgetIcon", "WidgetTitle", "Checkbox" };
            var to = new[] { Resource.Id.WidgetIcon, Resource.Id.WidgetTitle, Resource.Id.Checkbox };

            var listItems = PopulateEnabledWidgetsList();
            
            var adapter = new SimpleAdapter(this, listItems, Resource.Layout.EnabledWidgetsScreen_ListItem, from, to);

            _listView.Adapter = adapter;
        }

        private IList<IDictionary<string, object>> PopulateEnabledWidgetsList()
        {
            var widgets = _app.AvailableWidgets;

            IList<IDictionary<String, object>> listItems = new List<IDictionary<String, object>>();

            foreach (var widget in widgets)
            {
                IDictionary<String, object> keyValueMap = new Dictionary<String, object>();
                keyValueMap.Add("WidgetIcon", widget.Icon);
                keyValueMap.Add("WidgetTitle", widget.Name);
                keyValueMap.Add("Checkbox", widget.IsEnabled);
                listItems.Add(keyValueMap);
            }

            return listItems;
        }
    }
}