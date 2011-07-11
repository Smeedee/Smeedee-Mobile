using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Widgets.Settings
{
    [Activity(Label = "Top Committers Settings")]
    public class TopCommittersSettings : PreferenceActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            AddPreferencesFromResource(Resource.Layout.TopCommittersSettings);


            var preferences = PreferenceManager.GetDefaultSharedPreferences(this);

            var countView = FindViewById<ListPreference>(Resource.Id.TopCommittersCountPrefId);
            var countValue = preferences.GetString("TopCommittersCountPref", "5");
            /*
            countView.Summary = "Showing top " + countValue + " committers";
            */
            //countView.Summary = 
        }
    }
}