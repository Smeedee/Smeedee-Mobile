using Android.App;
using Android.OS;
using Android.Preferences;
using Smeedee.Model;

namespace Smeedee.Android.Widgets.Settings
{
    [Activity(Label = "Top Committers Settings", Theme = "@android:style/Theme")]
    public class TopCommittersSettings : PreferenceActivity
    {
        private TopCommitters _model;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            AddPreferencesFromResource(Resource.Layout.TopCommittersSettings);
            _model = new TopCommitters();
        }

        private void UpdateSummaryForPreferences()
        {
            var countPreference = (ListPreference)FindPreference(TopCommitters.NumberOfCommittersPropertyKey);
            var timePreference = (ListPreference)FindPreference(TopCommitters.TimePeriodPropertyKey);

            countPreference.Summary = string.Format("Top {0} committers", _model.NumberOfCommitters);
            timePreference.Summary = string.Format("Past {0}", _model.TimePeriod.ToSuffix());
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            UpdateSummaryForPreferences();
        }
        protected override void OnResume()
        {
            base.OnResume();
            UpdateSummaryForPreferences();
        }
    }
}