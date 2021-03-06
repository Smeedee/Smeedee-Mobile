﻿using System;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    [Widget("Latest Commits", StaticDescription = "Displays latest commits")]
    public class LatestCommitsWidget : IWpWidget
    {
        private LatestCommitsViewModel _latestCommitsViewModel;

        public LatestCommitsWidget()
        {
            _latestCommitsViewModel = new LatestCommitsViewModel();
            View = new LatestCommitsView { DataContext = _latestCommitsViewModel };
            Refresh();
        }

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
            throw new NotImplementedException();
        }

        public event EventHandler DescriptionChanged;

        public PivotItem View { get; set; }
    }
}
