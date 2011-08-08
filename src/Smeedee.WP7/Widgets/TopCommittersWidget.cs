using System;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
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
