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
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    public class LatestCommitsWidget : IWpWidget
    {
        public LatestCommitsWidget()
        {
            var latestCommitsViewModel = new LatestCommitsViewModel();
            latestCommitsViewModel.LoadData();
            View = new LatestCommitsView { DataContext = latestCommitsViewModel };
        }

        public void Refresh()
        {
            throw new NotImplementedException();
        }

        public DateTime LastRefreshTime()
        {
            throw new NotImplementedException();
        }

        public string GetDynamicDescription()
        {
            throw new NotImplementedException();
        }

        public event EventHandler DescriptionChanged;

        public FrameworkElement View { get; set; }
    }
}
