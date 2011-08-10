using System;
using System.Collections.ObjectModel;
using System.Windows;
using Smeedee.Model;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class BuildStatusViewModel : ViewModelBase
    {
        public ObservableCollection<BuildStatusItemViewModel> Items { get; private set; }
        private readonly BuildStatus model;

        public BuildStatusViewModel()
        {
            model = new BuildStatus();
            Items = new ObservableCollection<BuildStatusItemViewModel>();
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
            IsLoading = true;
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                Items.Clear();
                foreach (var build in model.Builds)
                {
                    var statusImageUri = "";

                    if (build.BuildSuccessState == BuildState.Broken) statusImageUri = "../Resources/Images/redx.png";
                    if (build.BuildSuccessState == BuildState.Working) statusImageUri = "../Resources/Images/checkmark.png";
                    if (build.BuildSuccessState == BuildState.Unknown) statusImageUri = "../Resources/Images/questionmark.png";

                    Items.Add(new BuildStatusItemViewModel 
                    {   ProjectName = build.ProjectName, 
                        UserName = build.Username, 
                        BuildTime = (DateTime.Now - build.BuildTime).PrettyPrint(),
                        BuildStatusImage = statusImageUri
                    });
                }
                IsLoading = false;
            }));
        }
    }
}
