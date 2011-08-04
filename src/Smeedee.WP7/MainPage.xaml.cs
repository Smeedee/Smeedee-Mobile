using System;
using System.Windows;
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

            WidgetsPivot.Items.Add(new SettingsWidget().View);
            WidgetsPivot.Items.Add(new LatestCommitsWidget().View);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
                App.ViewModel.LoadData();
        }

        private void SettingsIcon_Click(object sender, EventArgs e)
        {
            WidgetsPivot.Visibility = Visibility.Collapsed;
        }
    }
}