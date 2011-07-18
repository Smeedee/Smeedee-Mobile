using System;
using System.Collections.Generic;
using System.Linq;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Util;
using Android.Views;
using Android.Widget;
using Object = Java.Lang.Object;

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
            var from = new[] { "colorName" };
            var to = new[] { Resource.Id.lcs_checkedtextview };
            var items = GetEntries().Select(colorName => new Dictionary<string, object> {
                {"colorName", colorName}
            }).Cast<IDictionary<string, object>>().ToList();
            var colors = GetEntryValues().Select(ColorTools.GetColorFromHex).ToArray();
            return new TextColoringAdapter(Context, items, Resource.Layout.LatestChangesetsSettings_ListItem, from, to, colors);
        }
        
        internal class TextColoringAdapter : SimpleAdapter
        {
            private Color[] colors;

            public TextColoringAdapter(Context context, IList<IDictionary<string, object>> items, int resource, string[] from, int[] to, Color[] colors) :
                base(context, items, resource, from, to)
            {
                this.colors = colors;
                this.ViewBinder = new CustomViewBinder();
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

    internal class CustomViewBinder : SimpleAdapter.IViewBinder
    {
        public IntPtr Handle
        {
            get { throw new NotImplementedException(); }
        }

        public bool SetViewValue(View view, Object data, string textRepresentation)
        {
            if (!(view is CheckedTextView)) return false;
            var checkedView = view as CheckedTextView;
            checkedView.Checked = false;
            return true;
        }
    }
}