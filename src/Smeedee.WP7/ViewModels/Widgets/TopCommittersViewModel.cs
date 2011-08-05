using System;
using System.Linq;
using System.Windows;

using System.Collections.ObjectModel;
using Smeedee.Model;


namespace Smeedee.WP7.ViewModels.Widgets
{
    public class TopCommittersViewModel
    {
        public ObservableCollection<TopCommittersItemViewModel> Items { get; private set; }
        private readonly TopCommitters model;
        
        public TopCommittersViewModel()
       
        {
            model = new TopCommitters();
            Items = new ObservableCollection<TopCommittersItemViewModel>();
        }

        public void LoadData()
        {
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke( () =>
            {
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
            }));
        }
    }
}