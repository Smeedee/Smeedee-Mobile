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
            WidgetsPivot.Items.Add(new HomeScreenWidget().View);
            WidgetsPivot.Items.Add(new WorkingDaysLeftWidget().View);
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
                App.ViewModel.LoadData();
        }

        //private void list_LayoutUpdated(object sender, System.EventArgs e)
        //{
        //    var itemContainerGenerator = list.ItemContainerGenerator;
        //    if (itemContainerGenerator == null) return;

        //    if (list.ItemContainerGenerator == null) return;
        //    for (var i = 0; i < VisualTreeHelper.GetChildrenCount(list); i++)
        //    {
        //        var listBoxItem = list.ItemContainerGenerator.ContainerFromIndex(i) as ListBoxItem;
        //        if (listBoxItem == null) return;
        //        var progressBar1 = FindFirstElementInVisualTree<ProgressBar>(listBoxItem);
        //    }

            

        //    if (progressBar1 == null) return;
        //    progressBar1.Minimum = 0;
        //    progressBar1.Maximum = 100;
        //    progressBar1.Value = 39;
        //}
        ///*
        // * This method can be used to find the first element of a specific type in a parent element in the MainPage xaml.
        // * For instance we use this to find a ProgressBar inside a ListBoxItem. Just pass in the parent element, and 
        // * the element type you are searching for and you'll recieve it back.
        // */
        //private static T FindFirstElementInVisualTree<T>(DependencyObject parentElement) where T : DependencyObject
        //{
        //    var count = VisualTreeHelper.GetChildrenCount(parentElement);
        //    if (count == 0)
        //        return null;

        //    for (int i = 0; i < count; i++)
        //    {
        //        var child = VisualTreeHelper.GetChild(parentElement, i);

        //        if (child != null && child is T)
        //        {
        //            return (T)child;
        //        }
        //        else
        //        {
        //            var result = FindFirstElementInVisualTree<T>(child);
        //            if (result != null)
        //                return result;
        //        }
        //    }
        //    return null;
        //}

        private void SettingsIcon_Click(object sender, EventArgs e)
        {
            WidgetsPivot.Visibility = Visibility.Collapsed;
        }
    }
}