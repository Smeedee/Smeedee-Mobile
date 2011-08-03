using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Smeedee.WP7.ViewModels.Widgets;

namespace Smeedee.WP7
{
    public class LoadMoreListBox : ListBox
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item) {
            if (item == LatestCommitsViewModel.ButtonPlaceholderItem && element is ListBoxItem)
            {
                var loadMoreButton = new Button {Content = "Load more"};
                loadMoreButton.Click += (o, e) => App.ViewModel.LatestCommits.LoadMore();
                (element as ListBoxItem).Content = loadMoreButton;
            } else
            {
                base.PrepareContainerForItemOverride(element, item);
            }
        }
    }
}
