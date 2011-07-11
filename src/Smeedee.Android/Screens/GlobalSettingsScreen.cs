using Android.App;
using Android.OS;
using Android.Preferences;
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
                                       Checked = widgetModel.IsEnabled,
                                       Title = widgetModel.Name,
                                       Summary = widgetModel.DescriptionStatic,
                                       Key = widgetModel.Name
                                   };

                availableWidgetsCategory.AddPreference(checkBox);
            }
        }

        private void LoadPreferences()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            var serverUrl = (EditTextPreference) FindPreference("serverUrl");
            var userPassword = (EditTextPreference)FindPreference("userPassword");
            
            serverUrl.Summary = prefs.GetString("serverUrl", "Smeedee Server Url not set");
            userPassword.Summary = prefs.GetString("userPassword", "User password not set");
        }
    }
}