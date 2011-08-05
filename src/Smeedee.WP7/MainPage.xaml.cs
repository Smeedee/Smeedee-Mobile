using System;
using System.Windows;
using Microsoft.Phone.Controls;
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
            App.ViewModel.GetWidgetForView((PivotItem) WidgetsPivot.SelectedItem).Refresh();
        }
    }
}