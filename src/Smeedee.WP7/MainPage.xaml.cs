using System;
using System.Windows;
using Microsoft.Phone.Controls;
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7
{

    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            DataContext = App.ViewModel;
            Loaded += MainPage_Loaded;

            var view = new SettingsWidget().View;
            WidgetsPivot.Items.Add(view);
            //Console.WriteLine("TopCommitersListBox.Width: " + TopCommitersListBox.Width);
            //App.ViewModel.TopCommitters.CommitBarFullWidth = Convert.ToInt32(TopCommitersListBox.Width);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
            
        }

        private void SettingsIcon_Click(object sender, EventArgs e)
        {
            WidgetsPivot.Visibility = Visibility.Collapsed;
        }
    }
}