using Android.App;
using Android.OS;
using Android.Preferences;
using Smeedee.Android.Services;
using Smeedee.Model;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Smeedee settings", Theme = "@android:style/Theme")]
    public class GlobalSettings : PreferenceActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            AddPreferencesFromResource(Resource.Layout.GlobalSettingsScreen);
            
            LoadPreferences();
            PopulateAvailableWidgetsList();
        }
        private void PopulateAvailableWidgetsList()
        {
            var availableWidgetsCategory = (PreferenceScreen)FindPreference("availableWidgets");
            var widgets = SmeedeeApp.Instance.AvailableWidgets;
            foreach (var widgetModel in widgets)
            {
                if (widgetModel.Name == "Start Page") continue;

                var checkBox = new CheckBoxPreference(this)
                                   {
                                       Checked = true,
                                       Title = widgetModel.Name,
                                       Summary = widgetModel.DescriptionStatic,
                                       Key = widgetModel.Name
                                   };

                availableWidgetsCategory.AddPreference(checkBox);
            }
        }
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            LoadPreferences();
        }
        private void LoadPreferences()
        {
            var serverUrlPreference = (EditTextPreference) FindPreference("serverUrl");
            var userPasswordPreference = (EditTextPreference)FindPreference("userPassword");

            var database = new AndroidKVPersister(this);

            serverUrlPreference.Summary = database.Get("serverUrl");
            userPasswordPreference.Summary = database.Get("userPassword");
        }
    }
}