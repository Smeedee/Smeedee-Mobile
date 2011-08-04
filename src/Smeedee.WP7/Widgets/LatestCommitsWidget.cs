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
using Microsoft.Phone.Controls;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    public class LatestCommitsWidget : IWpWidget
    {
        private LatestCommitsViewModel _latestCommitsViewModel;

        public LatestCommitsWidget()
        {
            _latestCommitsViewModel = new LatestCommitsViewModel();
            View = new LatestCommitsView { DataContext = _latestCommitsViewModel };
            _latestCommitsViewModel.LoadData();
        }

        public void Refresh()
        {
            _latestCommitsViewModel.LoadData();
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

        public PivotItem View { get; set; }
    }
}
