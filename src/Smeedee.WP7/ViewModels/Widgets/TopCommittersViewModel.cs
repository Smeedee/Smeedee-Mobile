using System.Windows;

using System.Collections.ObjectModel;
using Smeedee.Model;


namespace Smeedee.WP7.ViewModels.Widgets
{
    public class TopCommittersViewModel : ViewModelBase
    {
        public TopCommittersViewModel()
        {
            model = new TopCommitters();
            Items = new ObservableCollection<TopCommittersItemViewModel>();
        }
        public ObservableCollection<TopCommittersItemViewModel> Items { get; private set; }
        private readonly TopCommitters model;

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke( () =>
            {
                foreach (var committer in model.Committers)
                {
                    Items.Add(new TopCommittersItemViewModel 
                    {   Name = committer.Name, 
                        Commits = committer.Commits + "", 
                        Image = committer.ImageUri
                    });
                }                      
            }));
            IsDataLoaded = true;
        }
    }
}