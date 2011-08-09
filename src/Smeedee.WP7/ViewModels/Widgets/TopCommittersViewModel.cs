using System;
using System.Linq;
using System.Windows;
using System.Collections.ObjectModel;
using Smeedee.Model;


namespace Smeedee.WP7.ViewModels.Widgets
{
    public class TopCommittersViewModel : ViewModelBase
    {
        public ObservableCollection<TopCommittersItemViewModel> Items { get; private set; }
        private readonly TopCommitters model;
        
        public TopCommittersViewModel()
       
        {
            model = new TopCommitters();
            Items = new ObservableCollection<TopCommittersItemViewModel>();
        }

        private bool _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (value != _isLoading)
                {
                    _isLoading = value;
                    NotifyPropertyChanged("IsLoading");
                }
            }
        }

        public void LoadData()
        {
            model.TimePeriod = TimePeriod.PastWeek;
            IsLoading = true;
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke( () =>
            {
                Items.Clear();
                foreach (var committer in model.Committers)
                {
                    var commitPercent = Convert.ToInt32((committer.Commits/(float)model.Committers.First().Commits)*100);

                    Items.Add(new TopCommittersItemViewModel 
                    {   Name = committer.Name, 
                        Commits = committer.Commits + "", 
                        Image = committer.ImageUri,
                        CommitPercent = commitPercent.ToString()
                    });
                }
                IsLoading = false;
            }));
        }
    }
}