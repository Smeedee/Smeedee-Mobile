using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Preferences;
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
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            AddPreferencesFromResource(Resource.Layout.GlobalSettingsScreen);
            
            
            PopulateAvailableWidgetsList();
        }

        private void PopulateAvailableWidgetsList()
        {
            var availableWidgetsCategory = (PreferenceScreen)FindPreference("GlobalSettings.AvailableWidgets");
            
            foreach (var widgetModel in SmeedeeApp.Instance.AvailableWidgets)
            {
                if (widgetModel.Type == typeof(StartPageWidget)) continue;

                var checkBox = new CheckBoxPreference(this)
                                   {
                                       Checked = widgetModel.Enabled,
                                       Title = widgetModel.Name,
                                       Summary = widgetModel.StaticDescription,
                                       Key = widgetModel.Name,
                                       OnPreferenceClickListener = new CheckBoxPreferenceClick(widgetModel)
                                   };
                availableWidgetsCategory.AddPreference(checkBox);
            }
        }
    }

    internal class CheckBoxPreferenceClick : Preference.IOnPreferenceClickListener
    {
        private readonly WidgetModel _widgetModel;
        public CheckBoxPreferenceClick(WidgetModel widgetModel)
        {
            _widgetModel = widgetModel;
        }
        public IntPtr Handle { get { throw new NotImplementedException(); } }

        public bool OnPreferenceClick(Preference preference)
        {
            _widgetModel.Enabled = ((CheckBoxPreference)preference).Checked;
            return true;
        }
    }
    public class ServerSettingsPreference : DialogPreference
    {
        private TextView _serverUrl;
        private TextView _userKey;
        private EditText _serverUrlBox;
        private EditText _userKeyBox;
        private readonly Context _context;

        private readonly IPersistenceService _persistence;

        public ServerSettingsPreference(IntPtr doNotUse) 
            : base(doNotUse)
        {
        }

        public ServerSettingsPreference(Context context, IAttributeSet attrs, int defStyle) 
            : base(context, attrs, defStyle)
        {
            _context = context;
            _persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
        }

        public ServerSettingsPreference(Context context, IAttributeSet attrs) 
            : base(context, attrs)
        {
            _context = context;
            _persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
        }
        protected override void OnBindDialogView(View view)
        {
            base.OnBindDialogView(view);
            _serverUrlBox.Text = _persistence.Get(Login.LoginUrl, "");
            _userKeyBox.Text = _persistence.Get(Login.LoginKey, "");
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

            _userKey = new TextView(_context) {Text = "User key:"};
            _userKey.SetTextColor(Color.White);

            _userKeyBox = new EditText(_context);
            _userKeyBox.SetSingleLine(true);

            layout.AddView(_serverUrl);
            layout.AddView(_serverUrlBox);
            layout.AddView(_userKey);
            layout.AddView(_userKeyBox);

            return layout; 
        }
        protected override void OnDialogClosed(bool positiveResult)
        {
            if (!positiveResult) return;

            var dialog = ProgressDialog.Show(_context, "", "Connecting to server and validating key...");
            var handler = new ProgressHandler(dialog);
            
            new Login().ValidateAndStore(_serverUrlBox.Text, _userKeyBox.Text, str 
                => ((Activity)_context).RunOnUiThread(() 
                    =>
                    {
                        if (str == Login.ValidationSuccess)
                            Toast.MakeText(_context, "Successfully connected to " + _serverUrlBox.Text, ToastLength.Long).Show();
                        else
                        {
                            Toast.MakeText(_context, "Connection failed. Please try again", ToastLength.Long).Show();
                            ShowDialog(null);
                        }
                        handler.SendEmptyMessage(0);                                                                                                                 
                    }));
        }
    }
}