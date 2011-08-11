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
using Smeedee.Android.Lib;
using Object = Java.Lang.Object;

namespace Smeedee.Android.Widgets.Settings
{
    [Activity(Label = "Latest Commits Settings", Theme = "@android:style/Theme")]
    public class LatestCommitsSettings : PreferenceActivity
    {
        public const string DefaultRed = "dc322f";
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Layout.LatestCommitsSettings);
            LoadPreferences();
        }

        private void LoadPreferences()
        {
            var prefs = PreferenceManager.GetDefaultSharedPreferences(this);

            var chosenColorAsHex = prefs.GetString("lcs_HighlightColor", DefaultRed);
            var highlightColorPrefs = (ColoredListPreference)FindPreference("lcs_HighlightColor");
            var colorNames = highlightColorPrefs.GetEntries();
            var colorValues = highlightColorPrefs.GetEntryValues();

            highlightColorPrefs.Summary = colorNames[Array.IndexOf(colorValues, chosenColorAsHex)];
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

            return new TextColoringAdapter(Context, items, Resource.Layout.LatestCommitsSettings_ListItem, from, to, colors);
        }

        private class TextColoringAdapter : SimpleAdapter
        {
            private readonly Color[] colors;

            public TextColoringAdapter(Context context, IList<IDictionary<string, object>> items, int resource, string[] from, int[] to, Color[] colors) :
                base(context, items, resource, from, to)
            {
                this.colors = colors;
                ViewBinder = new CheckedTextViewTextBinder();
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

        private class CheckedTextViewTextBinder : SimpleAdapter.IViewBinder
        {
            /* Android 2.1 crashes when binding the CheckedTextViews to text 
             * (it tries to bind the Checked property (a bool), not the Text). 
             * So we have to do that binding ourselves.
             * 
             * To see whats happening, compare the 2.1 implementation, line 189 here
             * http://grepcode.com/file/repository.grepcode.com/java/ext/com.google.android/android/2.1_r2/android/widget/SimpleAdapter.java#SimpleAdapter
             * 
             * With the 2.2 implementation, line 178 here:
             * http://grepcode.com/file/repository.grepcode.com/java/ext/com.google.android/android/2.2_r1.1/android/widget/SimpleAdapter.java#SimpleAdapter
             */
            public IntPtr Handle  { get { throw new NotImplementedException(); } }

            public bool SetViewValue(View view, Object data, string textRepresentation)
            {
                if (!(view is CheckedTextView)) return false;
                var checkedView = view as CheckedTextView;
                checkedView.Text = data.ToString();
                return true;
            }
        }
    }
}