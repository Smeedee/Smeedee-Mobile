using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Smeedee.Android.Widgets;
using Smeedee.Model;

namespace Smeedee.Android.Screens
{
    [Activity(Label = "Smeedee settings", Theme = "@android:style/Theme")]
    public class GlobalSettings : PreferenceActivity
    {
        private IPersistenceService persistence;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
            
            AddPreferencesFromResource(Resource.Layout.GlobalSettingsScreen);
            
            LoadPreferences();
            PopulateAvailableWidgetsList();
        }
        private void LoadPreferences()
        {
            var login = new Login();
            FindPreference(Login.LoginUrl).Summary = login.Url;
            FindPreference(Login.LoginKey).Summary = login.Key;
        }

        private void PopulateAvailableWidgetsList()
        {
            var availableWidgetsCategory = (PreferenceScreen)FindPreference("availableWidgets");
            var widgets = SmeedeeApp.Instance.AvailableWidgets;
            foreach (var widgetModel in widgets)
            {
                if (widgetModel.Type == typeof(StartPageWidget)) continue;

                var checkBox = new CheckBoxPreference(this)
                                   {
                                       Checked = true,
                                       Title = widgetModel.Name,
                                       Summary = widgetModel.StaticDescription,
                                       Key = widgetModel.Name
                                   };

                availableWidgetsCategory.AddPreference(checkBox);
            }
        }
        public override void OnWindowFocusChanged(bool hasFocus)
        {
            base.OnWindowFocusChanged(hasFocus);
            LoadPreferences();
        }
    }
    public class ServerSettingsPreference : DialogPreference
    {
        private TextView _serverUrl;
        private TextView _userKey;
        private EditText _serverUrlBox;
        private EditText _userKeyBox;
        private readonly Context _context;

        public ServerSettingsPreference(IntPtr doNotUse) 
            : base(doNotUse)
        {
        }

        public ServerSettingsPreference(Context context, IAttributeSet attrs, int defStyle) 
            : base(context, attrs, defStyle)
        {
            _context = context;
        }

        public ServerSettingsPreference(Context context, IAttributeSet attrs) 
            : base(context, attrs)
        {
            _context = context;
        }
        protected override View OnCreateDialogView()
        {
            var layout = new LinearLayout(_context) {Orientation = Orientation.Vertical};
            layout.SetPadding(10, 10, 10, 10);
            layout.SetBackgroundColor(Color.Black);

            _serverUrl = new TextView(_context) {Text = "Server url:"};
            _serverUrl.SetTextColor(Color.White);
            _serverUrl.SetPadding(0, 8, 0, 3);

            _serverUrlBox = new EditText(_context);
            _serverUrlBox.SetSingleLine(true);
            _serverUrlBox.SetSelectAllOnFocus(true);

            _userKey = new TextView(_context) {Text = "Key:"};
            _userKey.SetTextColor(Color.White);

            _userKeyBox = new EditText(_context);
            _userKeyBox.SetSingleLine(true);
            _userKeyBox.SetSelectAllOnFocus(true);

            layout.AddView(_serverUrl);
           layout.AddView(_serverUrlBox);
           layout.AddView(_userKey);
           layout.AddView(_userKeyBox);

            return layout; 
        }
        protected override void OnDialogClosed(bool positiveResult)
        {
            Toast.MakeText(_context, "Result from dialog: " + positiveResult.ToString(), ToastLength.Short).Show();
        }
    }
}