﻿using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Smeedee.WP7.ViewModels;

namespace Smeedee.WP7
{
    public partial class MainPage
    {
        private LoginViewModel _loginViewModel;

        public MainPage()
        {
            InitializeComponent();

            App.ViewModel.LoginIsVisible = true;
            DataContext = App.ViewModel;
            Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            _loginViewModel = App.ViewModel.LoginViewModel;
            _loginViewModel.OnSuccessfulValidation += (o, ev) =>
            {
                App.ViewModel.LoginIsVisible = false;
                App.ViewModel.MainPage = this;
                App.ViewModel.RecreatePivotControlIfChanged();
            };
            _loginViewModel.Validate();
        }

        private void SettingsIcon_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SettingsPage.xaml", UriKind.Relative));
        }
        
        private void RefreshButton_Click(object sender, EventArgs e)
        {
            var selected = (PivotItem)App.ViewModel.WidgetsPivot.SelectedItem;
            if (selected != null)
                App.ViewModel.GetWidgetForView(selected).Refresh();
        }

        public void LoginViewModel_Validate(object sender, EventArgs e)
        {
            _loginViewModel.Validate();
        }
    }
}