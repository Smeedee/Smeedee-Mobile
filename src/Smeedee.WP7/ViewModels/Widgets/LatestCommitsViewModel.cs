using System;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
                    Items.Add(new LatestCommitsItemViewModel { Message = commit.Message, User = commit.User, Date = commit.Date.ToString() });
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
