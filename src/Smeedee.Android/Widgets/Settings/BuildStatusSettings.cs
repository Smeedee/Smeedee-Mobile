using Android.App;
using Android.OS;
using Android.Preferences;


namespace Smeedee.Android.Widgets.Settings
{
    [Activity(Label = "BuildStatusSettings")]
    public class BuildStatusSettings : PreferenceActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            AddPreferencesFromResource(Resource.Layout.BuildStatusSettings);
        }
    }
}