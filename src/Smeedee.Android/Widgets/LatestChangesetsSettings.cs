using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Object = System.Object;
using String = System.String;

namespace Smeedee.Android.Widgets
{
    [Activity(Label = "Latest Changesets Settings", MainLauncher = true, Theme = "@android:style/Theme.NoTitleBar",
        Icon = "@drawable/icon_smeedee")]
    public class LatestChangesetsSettings : PreferenceActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            AddPreferencesFromResource(Resource.Layout.LatestChangesetsSettings);
        }
    }

    public class CustomListPreference : ListPreference
    {
        public CustomListPreference(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }
        
        protected override void OnPrepareDialogBuilder(AlertDialog.Builder builder)
        {
            Log.Debug("SMEEDEE", "OnCreateDialogView returning null:");
            var entries = GetEntries();
            var entryValues = GetEntryValues();
            if (entries == null || entryValues == null) {
                throw new IllegalStateException(
                        "ListPreference requires an entries array and an entryValues array.");
            }

            var clickedDialogEntryIndex = FindIndexOfValue(Value);
            var self = this;
            EventHandler<DialogClickEventArgs> onClick = (o, e) =>
                              {
                                  //mClickedDialogEntryIndex = which;
                                  //ListPreference.this.onClick(dialog, DialogInterface.BUTTON_POSITIVE);
                                  //self.OnClick(dialog, DialogInterface.BUTTON_POSITIVE);
                                  //self.OnClick(null, e.Which);
                                  //dialog.dismiss();
                              };
            var adapter = CreateAdapter();
            builder.SetSingleChoiceItems(adapter, clickedDialogEntryIndex, onClick);
            
            /*
             * The typical interaction for list-based dialogs is to have
             * click-on-an-item dismiss the dialog instead of the user having to
             * press 'Ok'.
             */
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
            Func<int, Color> colorFn = (position) =>
                                           {
                                               var hex = (string) items[position]["colorValue"];
                                               var r = Integer.ParseInt(hex.Substring(0, 2), 16);
                                               var g = Integer.ParseInt(hex.Substring(2, 2), 16);
                                               var b = Integer.ParseInt(hex.Substring(4, 2), 16);
                                               return new Color(r, g, b);
                                           };
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
                return view;
            }
        }
    }
}