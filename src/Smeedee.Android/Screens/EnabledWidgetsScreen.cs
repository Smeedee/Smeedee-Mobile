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
    [Activity(Label = "EnabledWidgetsScreen", Theme = "@android:style/Theme.NoTitleBar")]
    public class EnabledWidgetsScreen : Activity, View.IOnClickListener
    {
        private readonly SmeedeeApp _app = SmeedeeApp.Instance;
        private Button _saveSettingsBtn;
        private bool _firstTimeUserOpensThisActivity = true;
        private ListView _listView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.EnabledWidgetsScreen);

            _saveSettingsBtn = FindViewById<Button>(Resource.Id.BtnSaveSettings);

            if (_firstTimeUserOpensThisActivity) ViewSaveButton();
            else HideSaveButton();

            _listView = FindViewById<ListView>(Resource.Id.EnabledWidgetsScreenBuildList);

            var from = new[] { "WidgetIcon", "WidgetTitle", "Checkbox" };
            var to = new[] { Resource.Id.WidgetIcon, Resource.Id.WidgetTitle, Resource.Id.Checkbox };

            var listItems = PopulateEnabledWidgetsList();

            
            var adapter = new SimpleAdapter(this, listItems, Resource.Layout.EnabledWidgetsScreen_ListItem, from, to);

            //TODO: _listViewSetOnClickListener(this) and handle list clicks
            _listView.Adapter = adapter;
        }

        private void ViewSaveButton()
        {
            var saveBtn = FindViewById(Resource.Id.BtnSaveSettings);
            saveBtn.Visibility = ViewStates.Visible;
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

        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }
    }
}