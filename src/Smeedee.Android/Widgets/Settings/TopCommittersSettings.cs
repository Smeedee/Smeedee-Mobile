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
            UpdateSummaryForPreferences();
        }

        private void UpdateSummaryForPreferences()
        {
            UpdateSummaryForCount();
            UpdateSummaryForTime();
        }

        private void UpdateSummaryForCount()
        {
            var countPreference = (ListPreference)FindPreference("TopCommitters.NumberOfCommitters");
            countPreference.Summary = string.Format("Top {0} committers", _model.NumberOfCommitters);
        }

        private void UpdateSummaryForTime()
        {
            var timePreference = (ListPreference)FindPreference("TopCommitters.TimePeriod");
            var time = _model.TimePeriod;
            var suffix = (time == TimePeriod.PastDay) ? "24 hours" : (time == TimePeriod.PastWeek) ? "week" : "month";
            timePreference.Summary = string.Format("Past {0}", suffix);
        }

        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            UpdateSummaryForPreferences();
        }
    }
}