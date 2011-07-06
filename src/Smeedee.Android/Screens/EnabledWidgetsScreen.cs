using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Smeedee.Model;
using Object = Java.Lang.Object;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "EnabledWidgetsScreen", Theme = "@android:style/Theme.NoTitleBar")]
    public class EnabledWidgetsScreen : Activity
    {
        private readonly SmeedeeApp app = SmeedeeApp.Instance;
        private Button _saveSettingsBtn;
        private bool _firstTimeUserOpensThisActivity = true;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EnabledWidgetsScreen);

            _saveSettingsBtn = FindViewById<Button>(Resource.Id.BtnSaveSettings);

            if (_firstTimeUserOpensThisActivity) ViewSaveButton();
            else HideSaveButton();

            var listView = FindViewById<ListView>(Resource.Id.EnabledWidgetsScreenBuildList);

            var from = new[] { "WidgetIcon", "WidgetTitle" };
            var to = new[] { Resource.Id.WidgetIcon, Resource.Id.WidgetTitle };

            var listItems = PopulateEnabledWidgetsList();

            
            var adapter = new SimpleAdapter(this, listItems, Resource.Layout.EnabledWidgetsScreen_ListItem, from, to);
            listView.Adapter = adapter;
        }

        private void ViewSaveButton()
        {
            BindSaveButtonClickEvent();
            _firstTimeUserOpensThisActivity = false;
        }

        private void BindSaveButtonClickEvent()
        {
            _saveSettingsBtn.Click += delegate(object sender, EventArgs args)
            {
                //TODO: Iterate over the list and instansiate/deinstansiate the widgets the user has checked

                var widgetContainer = new Intent(this, typeof(WidgetContainer));
                StartActivity(widgetContainer);
            };
        }

        private void HideSaveButton()
        {
            _saveSettingsBtn.Visibility = ViewStates.Invisible;
        }

        private IList<IDictionary<string, object>> PopulateEnabledWidgetsList()
        {
            var widgets = app.AvailableWidgets;

            IList<IDictionary<String, object>> listItems = new List<IDictionary<String, object>>();

            foreach (var widget in widgets)
            {
                IDictionary<String, object> keyValueMap = new Dictionary<String, object>();
                keyValueMap.Add("WidgetIcon", widget.Icon);
                keyValueMap.Add("WidgetTitle", widget.Name);
                listItems.Add(keyValueMap);
            }

            return listItems;
        }
    }
}