﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using Smeedee.Model;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class BuildStatusViewModel : ViewModelBase, IWidget
    {
        public ObservableCollection<BuildStatusItemViewModel> Items { get; private set; }
        private readonly BuildStatus model;

        public BuildStatusViewModel()
        {
            model = new BuildStatus();
            Items = new ObservableCollection<BuildStatusItemViewModel>();
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                foreach (var build in model.Builds)
                {
                    Items.Add(new BuildStatusItemViewModel { ProjectName = build.ProjectName, UserName = build.Username, BuildTime = build.BuildTime.ToString()});
                }
            }));
            IsDataLoaded = true;
        }

        public void Refresh()
        {
            LoadData();
        }

        private DateTime lastRefreshTime;
        private string dynamicDescription;

        public DateTime LastRefreshTime()
        {
            return lastRefreshTime;
        }

        public string GetDynamicDescription()
        {
            return dynamicDescription;
        }

        public event EventHandler DescriptionChanged;
    }
}