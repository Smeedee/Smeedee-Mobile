using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Smeedee.WP7.ViewModels.Widgets;

namespace Smeedee.WP7
{
    public class LoadMoreListBox : ListBox
    {
        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            if (item == null || item == LatestCommitsViewModel.ButtonPlaceholderItem && element is ListBoxItem)
            {
                var listBoxItem = (element as ListBoxItem);
                var loadMoreButton = new Button { Content = "Load more" };
                loadMoreButton.Click += (o, e) =>
                {
                    loadMoreButton.Opacity = 0;
                    (DataContext as LatestCommitsViewModel).LoadMore();
                };
                loadMoreButton.IsEnabled = (DataContext as LatestCommitsViewModel).LoadMoreButtonIsEnabled;
                listBoxItem.Content = loadMoreButton;
            }
            else
            {
                base.PrepareContainerForItemOverride(element, item);
            }
        }
    }
}
