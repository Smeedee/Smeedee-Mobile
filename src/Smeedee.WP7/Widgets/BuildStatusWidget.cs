using System;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    [Widget("Build Status", StaticDescription = "Shows build status for each project")]
    public class BuildStatusWidget : IWpWidget
    {
        private BuildStatusViewModel _buildStatusViewModel;

        public BuildStatusWidget()
        {
            _buildStatusViewModel = new BuildStatusViewModel();
            View = new BuildStatusView { DataContext = _buildStatusViewModel };
            Refresh();
        }

        public PivotItem View { get; set; }

        public void Refresh()
        {
            _lastRefreshTime = DateTime.Now;
            _buildStatusViewModel.LoadData();
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
