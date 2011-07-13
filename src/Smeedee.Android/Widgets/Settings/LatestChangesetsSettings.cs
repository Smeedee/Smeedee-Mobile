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
using Java.Lang;


namespace Smeedee.Android.Widgets.Settings
{
    [Activity(Label = "Latest Changesets Settings", Theme = "@android:style/Theme")]
    public class LatestChangesetsSettings : PreferenceActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Layout.LatestChangesetsSettings);
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
            var items = new List<IDictionary<string, object>>();
            var from = new[] { "colorName" };
            var to = new[] { Resource.Id.lcs_checkedtextview };
            for (var i = 0; i < entries.Length; ++i)
            {
                items.Add(new Dictionary<string, object>()
                              {
                                  {"colorName", entries[i]},
                                  {"colorValue", entryValues[i]}
                              });
            }
            Func<int, Color> colorFn = position => ColorTools.FromHex(entryValues[position]);
            return new TextColoringAdapter(Context, items, Resource.Layout.LatestChangesetsSettings_ListItem, from, to, colorFn);
        }
        
        internal class TextColoringAdapter : SimpleAdapter
        {
            private Func<int, Color> colorFn;

            public TextColoringAdapter(Context context, IList<IDictionary<string, object>> items, int resource, string[] from, int[] to, Func<int, Color> colorFn) :
                base(context, items, resource, from, to)
            {
                this.colorFn = colorFn;
            }

            public override View GetView(int position, View convertView, ViewGroup parent)
            {
                var view = base.GetView(position, convertView, parent);
                if (!(view is CheckedTextView)) return view;

                var checkedTextView = (view as CheckedTextView);
                checkedTextView.SetTextColor(colorFn(position));
                checkedTextView.SetBackgroundColor(Color.Black);
                return view;
            }
        }
    }
}