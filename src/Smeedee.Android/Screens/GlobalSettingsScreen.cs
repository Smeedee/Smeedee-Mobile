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
        /* TODO: 
         * We can't rely on storing in DefaultSharedPreference here, because we
         * appearantly can't access that from MainActivity and LoginScreen.
         * Thus we need to push the values into the IPersistenceService global storage
         * when they are changed here. 
         * 
         * At the moment, this settings screen doesn't affect the app at all:(
         */
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
            var serverUrlSummaryPref = (EditTextPreference) FindPreference("serverUrl");
            var userPasswordSummaryPref = (EditTextPreference)FindPreference("userPassword");
            
            var pref = PreferenceManager.GetDefaultSharedPreferences(this);
           
            serverUrlSummaryPref.Summary = pref.GetString("serverUrl", "url");
            userPasswordSummaryPref.Summary = pref.GetString("userPassword", "pass");
        }
    }
}