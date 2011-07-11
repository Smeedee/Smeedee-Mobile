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
using Smeedee.Model;
using Smeedee.Services;
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
            var colorPref = (CustomListPreference)FindPreference("lcs_customListPref");
            Log.Debug("SMEEDEE", "colorPref.Value: " + colorPref.Value);
            //TODO: Deal with persisting the setting
        }
    }

    public class CustomListPreference : ListPreference
    {
        private IPersistenceService persistence;
        public CustomListPreference(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
            //persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
            //persistence.Get("latest_changesets.color_index", 2);
        }
        
        protected override void OnPrepareDialogBuilder(AlertDialog.Builder builder)
        {
            var clickedDialogEntryIndex = FindIndexOfValue(Value);
            Log.Debug("SMEEDEE", "onPrepare Value " + Value);
            Log.Debug("SMEEDEE", "clickedDialogEntryIndex " + clickedDialogEntryIndex);
            EventHandler<DialogClickEventArgs> onClick = (o, e) =>
                              {
                                  Log.Debug("SMEEDEE", "Value pre: " + Value);
                                  var dialog = (AlertDialog) o;
                                  var selectedIndex = (int) e.Which;
                                  SetValueIndex(selectedIndex);
                                  OnClick(dialog, DialogInterfaceButton.Positive);
                                  dialog.Dismiss();
                                  Log.Debug("SMEEDEE", "Value post: " + Value);
                              };
            var adapter = CreateAdapter();
            builder.SetSingleChoiceItems(adapter, clickedDialogEntryIndex, onClick);
            
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
                checkedTextView.SetBackgroundColor(Color.Black);
                return view;
            }
        }
    }
}