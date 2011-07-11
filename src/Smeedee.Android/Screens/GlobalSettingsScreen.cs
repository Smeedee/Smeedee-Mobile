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

            PopulateAvailableWidgetsCheckboxes();

            //TODO: Show the last stored url and user password in EditTextPreference.DefaultValue
        }

        private void PopulateAvailableWidgetsCheckboxes()
        {
            var availableWidgetsCategory = (PreferenceScreen)FindPreference("availableWidgets");
            var widgets = SmeedeeApp.Instance.AvailableWidgets;
            foreach (var widgetModel in widgets)
            {
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

    }
}