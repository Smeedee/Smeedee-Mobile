using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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