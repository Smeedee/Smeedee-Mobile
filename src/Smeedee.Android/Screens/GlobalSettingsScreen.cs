using Android.App;
using Android.OS;
using Android.Preferences;
using Smeedee.Android.Widgets;
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
                if (widgetModel.Type == typeof(StartPageWidget)) continue;

                var checkBox = new CheckBoxPreference(this)
                                   {
                                       Checked = true,
                                       Title = widgetModel.Name,
                                       Summary = widgetModel.StaticDescription,
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
            var slideShowIntervalPref = (ListPreference) FindPreference("slideShowInterval");
            var serverUrlSummaryPref = (EditTextPreference) FindPreference("serverUrl");
            var userPasswordSummaryPref = (EditTextPreference)FindPreference("userPassword");

            var pref = PreferenceManager.GetDefaultSharedPreferences(this);

            slideShowIntervalPref.Summary = pref.GetString("slideShowInterval", "20000") + " milliseconds";
            serverUrlSummaryPref.Summary = pref.GetString("serverUrl", "url");
            userPasswordSummaryPref.Summary = pref.GetString("userPassword", "pass");
        }
    }
}