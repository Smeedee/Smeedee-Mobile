using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Widgets.Settings
{
    [Activity(Label = "Top Committers Settings")]
    public class TopCommittersSettings : PreferenceActivity
    {
        private ISharedPreferences preferences;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            AddPreferencesFromResource(Resource.Layout.TopCommittersSettings);

            preferences = PreferenceManager.GetDefaultSharedPreferences(this);
            UpdateSummaryForPreferences();

        }

        private void UpdateSummaryForPreferences()
        {
            UpdateSummaryForCount();
            UpdateSummaryForTime();
        }

        private void UpdateSummaryForCount()
        {
            var countPreference = FindPreference("TopCommittersCountPref") as ListPreference;
            var val = preferences.GetString("TopCommittersCountPref", "5");

            if (countPreference != null)
            {
                countPreference.Summary = "Showing top " + val + " committers";
            }
            else
            {
                throw new NullReferenceException("Could not find Top committers 'count' preference");
            }
        }

        private void UpdateSummaryForTime()
        {
            var timePreference = FindPreference("TopCommittersTimePref") as ListPreference;
            var val = preferences.GetString("TopCommittersTimePref", "1");

            if (timePreference != null)
            {
                switch (val)
                {
                    case "1":
                        timePreference.Summary = "Showing top committers for the past 24 hours";
                        break;
                    case "7":
                        timePreference.Summary = "Showing top committers for the past week";
                        break;
                    case "30":
                        timePreference.Summary = "Showing top committers for the past month";
                        break;
                }
            }
            else
            {
                throw new NullReferenceException("Could not find Top committers 'count' preference");
            }
        }
    }
}