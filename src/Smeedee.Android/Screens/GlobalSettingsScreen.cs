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
        private IPersistenceService persistence;
        private SmeedeeApp app = SmeedeeApp.Instance;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            persistence = app.ServiceLocator.Get<IPersistenceService>();

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
            var login = new Login();
            FindPreference(Login.LoginUrl).Summary = login.Url;
            FindPreference(Login.LoginKey).Summary = login.Key;
        }
    }
}