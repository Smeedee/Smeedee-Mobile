using Android.App;
using Android.OS;
using Android.Preferences;
using Smeedee.Model;

namespace Smeedee.Android.Widgets.Settings
{
    [Activity(Label = "Build Status Settings", Theme = "@android:style/Theme")]
    public class BuildStatusSettings : PreferenceActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            AddPreferencesFromResource(Resource.Layout.BuildStatusSettings);
            LoadPreferences();
        }
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            LoadPreferences();
        }
        private void LoadPreferences()
        {
            var buildOrderPreference = (ListPreference)FindPreference(BuildStatus.SortingPropertyKey);
            var pref = PreferenceManager.GetDefaultSharedPreferences(this);
            var sortOn = pref.GetString(BuildStatus.SortingPropertyKey, "buildtime");
            if (sortOn == "buildtime")
                buildOrderPreference.Summary = "Order by build time";
            if (sortOn == "projectname")
                buildOrderPreference.Summary = "Order by project name";
        }
    }
}