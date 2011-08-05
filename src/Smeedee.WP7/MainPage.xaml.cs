using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7
{
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            DataContext = App.ViewModel;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var loginViewModel = App.ViewModel.LoginViewModel;
            loginViewModel.IsValidating = true;
            new Login().IsValid(valid => Dispatcher.BeginInvoke(() =>
            {
                loginViewModel.IsValidating = false;
                if (valid)
                {
                    App.ViewModel.LoadWidgets();
                    LoginScreen.Visibility = Visibility.Collapsed;
                    WidgetsPivot.Visibility = Visibility.Visible;
                } else
                {
                    loginViewModel.ValidationFailed = true;
                }
            }));
        }

        private void SettingsIcon_Click(object sender, EventArgs e)
        {
            App.ViewModel.SettingsAreShowing = true;
        }

        private void WidgetsButton_Click(object sender, EventArgs e)
        {
            App.ViewModel.WidgetsAreShowing = true;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            var selected = (PivotItem)WidgetsPivot.SelectedItem;
            if (selected != null)
                App.ViewModel.GetWidgetForView(selected).Refresh();
        }
    }
}