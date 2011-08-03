﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using Smeedee.Model;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class LatestCommitsViewModel : IWidget
    {
        public ObservableCollection<LatestCommitsItemViewModel> Items { get; private set; }
        private readonly LatestCommits model;

        public LatestCommitsViewModel()
        {
            model = new LatestCommits();
            Items = new ObservableCollection<LatestCommitsItemViewModel>();
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
                foreach (var commit in model.Commits)
                {
                    Items.Add(new LatestCommitsItemViewModel { Message = commit.Message, User = commit.User, Date = (DateTime.Now - commit.Date).PrettyPrint(), Image = commit.ImageUri });
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
