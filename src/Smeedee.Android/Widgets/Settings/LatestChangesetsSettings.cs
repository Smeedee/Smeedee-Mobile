using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Smeedee.Android.Widgets.Settings
{
    [Activity(Label = "Latest Commits Settings", Theme = "@android:style/Theme")]
    public class LatestChangesetsSettings : PreferenceActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Layout.LatestChangesetsSettings);
            LoadPreferences();
        }

        private void LoadPreferences()
        {
            var numberOfCommitsDisplayed = (ListPreference) FindPreference("NumberOfCommitsDisplayed");
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);
            numberOfCommitsDisplayed.Summary = "Displaying latest "
                + prefs.GetString("NumberOfCommitsDisplayed", "10") + " commits";

            var highlightColorSummary = (ColoredListPreference)FindPreference("lcs_HighlightColor");
            var chosenColorAsHex = prefs.GetString("lcs_HighlightColor", "dc322f");
            
            var colorNames = highlightColorSummary.GetEntries();
            var colorValues = highlightColorSummary.GetEntryValues();
            
            for (int i = 0; i < colorValues.Length; i++)
            {
                if (colorValues[i] == chosenColorAsHex)
                    highlightColorSummary.Summary = colorNames[i];
            }
        }
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            LoadPreferences();
        }
    }

    public class ColoredListPreference : ListPreference
    {
        public ColoredListPreference(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }
        
        protected override void OnPrepareDialogBuilder(AlertDialog.Builder builder)
        {
            EventHandler<DialogClickEventArgs> onClick = (o, e) =>
                              {
                                  var dialog = (AlertDialog) o;
                                  SetValueIndex((int) e.Which);
                                  OnClick((AlertDialog) o, DialogInterfaceButton.Positive);
                                  dialog.Dismiss();
                              };
            var adapter = CreateAdapter();
            builder.SetSingleChoiceItems(adapter, FindIndexOfValue(Value), onClick);
            
            HideOkButton(builder);
        }

        protected override void OnDialogClosed(bool positiveResult)
        {
            //do nothing, the base class resets the Value property in this method, we don't want that.
        }

        private static void HideOkButton(AlertDialog.Builder builder)
        {
            builder.SetPositiveButton("", (o, e) => { });
        }

        private TextColoringAdapter CreateAdapter()
        {
            var entries = GetEntries();
            var entryValues = GetEntryValues();

            var from = new[] { "colorName" };
            var to = new[] { Resource.Id.lcs_checkedtextview };
            
            var items = new List<IDictionary<string, object>>();
            for (var i = 0; i < entries.Length; ++i)
            {
                items.Add(new Dictionary<string, object>()
                              {
                                  {"colorName", entries[i]},
                                  {"colorValue", entryValues[i]}
                              });
            }
            var colors = new Dictionary<int, Color>();
            var index = 0;
            foreach (var colorValue in entryValues)
            {
                colors.Add(index, ColorTools.GetColorFromHex(colorValue));
                index++;
            }
            return new TextColoringAdapter(Context, items, Resource.Layout.LatestChangesetsSettings_ListItem, from, to, colors);
        }
        
        internal class TextColoringAdapter : SimpleAdapter
        {
            private IDictionary<int, Color> colors;

            public TextColoringAdapter(Context context, IList<IDictionary<string, object>> items, int resource, string[] from, int[] to, IDictionary<int, Color> colors) :
                base(context, items, resource, from, to)
            {
                this.colors = colors;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var view = base.GetView(position, convertView, parent);
                if (!(view is CheckedTextView)) return view;

                var checkedTextView = (view as CheckedTextView);
                checkedTextView.SetTextColor(colors[position]);
                checkedTextView.SetBackgroundColor(Color.Black);
                return view;
            }
        }
    }
}