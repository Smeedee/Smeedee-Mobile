using System;
using Microsoft.Phone.Controls;
using Smeedee.WP7.ViewModels.Settings;

namespace Smeedee.WP7
{
    public partial class SettingsPage : PhoneApplicationPage
    {
        private SettingsPageViewModel _settingsPageViewModel;

        public SettingsPage()
        {
            _settingsPageViewModel = new SettingsPageViewModel();
            InitializeComponent();
            DataContext = _settingsPageViewModel;
            BackKeyPress += (o, e) => App.ViewModel.OnExitSettings();
        }

        public void LoginViewModel_Validate(object sender, EventArgs e)
        {
            _settingsPageViewModel.LoginViewModel.Validate();
        }
    }
}