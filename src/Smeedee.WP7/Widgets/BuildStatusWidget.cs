﻿using System;
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
    public class BuildStatusWidget : IWpWidget
    {
        private BuildStatusViewModel _buildStatusViewModel;

        public BuildStatusWidget()
        {
            _buildStatusViewModel = new BuildStatusViewModel();
            View = new BuildStatusView() { DataContext = _buildStatusViewModel };
            _buildStatusViewModel.LoadData();
        }

        public PivotItem View { get; set; }

        public void Refresh()
        {
            _buildStatusViewModel.LoadData();
        }

        public DateTime LastRefreshTime()
        {
            return DateTime.Now;
        }

        public string GetDynamicDescription()
        {
            return "";
        }

        public event EventHandler DescriptionChanged;

    }
}