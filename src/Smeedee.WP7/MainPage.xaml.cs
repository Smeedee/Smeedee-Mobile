using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7
{
    public partial class MainPage
    {
        private LoginViewModel _loginViewModel;

        public MainPage()
        {
            InitializeComponent();

            App.ViewModel.WidgetsAreShowing = false;
            DataContext = App.ViewModel;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _loginViewModel = App.ViewModel.LoginViewModel;
            _loginViewModel.OnSuccessfulValidation += (o, ev) =>
            {
                App.ViewModel.WidgetsAreShowing = true;
            };
            _loginViewModel.Validate();
        }

        private void SettingsIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }

        private void WidgetsButton_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            var selected = (PivotItem)WidgetsPivot.SelectedItem;
            if (selected != null)
                App.ViewModel.GetWidgetForView(selected).Refresh();
        }

        public void LoginViewModel_Validate(object sender, EventArgs e)
        {
            _loginViewModel.Validate();
        }
    }
}