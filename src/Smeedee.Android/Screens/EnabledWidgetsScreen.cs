using System;
using System.Collections;
using System.Collections.Generic;
using Android.App;
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
        private SimpleAdapter adapter;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EnabledWidgetsScreen);

            var listView = FindViewById<ListView>(Resource.Id.EnabledWidgetsScreenBuildList);

            var from = new[] { "WidgetIcon", "WidgetTitle", "WidgetDescriptionStatic", "Checkbox" };
            var to = new[] { Resource.Id.WidgetIcon, Resource.Id.WidgetTitleEnabledWidgetsScreen, Resource.Id.WidgetDescriptionStaticEnabledWidgetsScreen, Resource.Id.Checkbox };

            var listItems = PopulateEnabledWidgetsList();
            
            adapter = new SimpleAdapter(this, listItems, Resource.Layout.EnabledWidgetsScreen_ListItem, from, to);
            listView.Adapter = adapter;
            listView.Clickable = true;
            
            listView.ItemClick += (o, e) =>
                                      {
                                          //((CheckBox) ((RelativeLayout) o).GetChildAt(2)).Checked = !((CheckBox) ((RelativeLayout) o).GetChildAt(2)).Checked;
                                          //TODO: Fix cleaner solution of this
                                          
                                          RunOnUiThread(() =>
                                                            {
                                                                ((IDictionary<string, object>)adapter.GetItem(e.Position))["Checkbox"] =
                                                                    !((bool)((IDictionary<string, object>)adapter.GetItem(e.Position))["Checkbox"]);
                                                                listView.Adapter = adapter;
                                                            });
                                          _app.AvailableWidgets[e.Position].IsEnabled = !_app.AvailableWidgets[e.Position].IsEnabled;
                                          
                                          //Save to storage

                                      };
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
                keyValueMap.Add("WidgetDescriptionStatic", widget.DescriptionStatic);
                keyValueMap.Add("Checkbox", widget.IsEnabled);
                listItems.Add(keyValueMap);
            }

            return listItems;
        }
    }
}