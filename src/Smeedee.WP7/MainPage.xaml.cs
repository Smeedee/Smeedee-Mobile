using System;
using System.Windows;
using Microsoft.Phone.Controls;

namespace Smeedee.WP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            DataContext = App.ViewModel;
            Loaded += MainPage_Loaded;

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
    }
}