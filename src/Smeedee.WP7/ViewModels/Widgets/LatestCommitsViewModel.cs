using System;
using System.Collections.ObjectModel;
using System.Windows;
using Smeedee.Model;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class LatestCommitsViewModel : ViewModelBase
    {
        public ObservableCollection<LatestCommitsItemViewModel> Items { get; private set; }
        private readonly LatestCommits model;

        public bool LoadMoreButtonIsEnabled
        {
            get
            {
                return model.HasMore;
            }
        }

        public LatestCommitsViewModel()
        {
            model = new LatestCommits();
            Items = new ObservableCollection<LatestCommitsItemViewModel>();
        }
        
        public void LoadData()
        {
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke(StoreDataFromModel));
        }

        public void LoadMore()
        {
            model.LoadMore(() => Deployment.Current.Dispatcher.BeginInvoke(StoreDataFromModel));
        }

        public static LatestCommitsItemViewModel ButtonPlaceholderItem = new LatestCommitsItemViewModel() {Image = new Uri("smeedee://placeholder")};


        private void StoreDataFromModel()
        {
            Items.Clear();
            foreach (var commit in model.Commits)
            {
                Items.Add(new LatestCommitsItemViewModel { Message = commit.Message, User = commit.User, Date = (DateTime.Now - commit.Date).PrettyPrint(), Image = commit.ImageUri });
            }
            Items.Add(null);
            //Items.Add(ButtonPlaceholderItem);
            NotifyPropertyChanged("LoadMoreButtonIsEnabled");
        }
    }
}
