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
using Smeedee.Model;
using Smeedee.WP7.Views;
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7.ViewModels.Widgets
{
    [Widget("Top Committers", StaticDescription = "A list of most active committers")]
    public class TopCommittersWidget : IWpWidget
    {
        private TopCommittersViewModel _latestCommitsViewModel;

        public TopCommittersWidget()
        {
            _latestCommitsViewModel = new TopCommittersViewModel();
            View = new TopCommittersView { DataContext = _latestCommitsViewModel };
            Refresh();
        }

        public PivotItem View { get; set; }

        public void Refresh()
        {
            _lastRefreshTime = DateTime.Now;
            _latestCommitsViewModel.LoadData();
        }

        private DateTime _lastRefreshTime;
        public DateTime LastRefreshTime()
        {
            return _lastRefreshTime;
        }

        public string GetDynamicDescription()
        {
            return "";
        }

        public event EventHandler DescriptionChanged;

    }
}
