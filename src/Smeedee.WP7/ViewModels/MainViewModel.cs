using Smeedee.WP7.ViewModels.Settings;
using Smeedee.WP7.ViewModels.Widgets;

namespace Smeedee.WP7.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public bool IsDataLoaded;
        
        private TopCommittersViewModel _topCommitters = new TopCommittersViewModel();
        public TopCommittersViewModel TopCommitters
        {
            get
            {
                return _topCommitters;
            }
            set
            {
                if (value != _topCommitters)
                {
                    _topCommitters = value;
                    NotifyPropertyChanged("TopCommitters");
                }
            }
        }
        private BuildStatusViewModel _buildStatus = new BuildStatusViewModel();
        public BuildStatusViewModel BuildStatus
        {
            get
            {
                return _buildStatus;
            }
            set
            {
                if (value != _buildStatus)
                {
                    _buildStatus = value;
                    NotifyPropertyChanged("BuildStatus");
                }
            }
        }

        private WorkingDaysLeftViewModel _workingDaysLeft = new WorkingDaysLeftViewModel();
        public WorkingDaysLeftViewModel WorkingDaysLeft
        {
            get
            {
                return _workingDaysLeft;
            }
            set
            {
                if (value != _workingDaysLeft)
                {
                    _workingDaysLeft = value;
                    NotifyPropertyChanged("WorkingDaysLeft");
                }
            }
        }

        private LatestCommitsViewModel _latestCommits = new LatestCommitsViewModel();
        public LatestCommitsViewModel LatestCommits
        {
            get
            {
                return _latestCommits;
            }
            set
            {
                if (value != _latestCommits)
                {
                    _latestCommits = value;
                    NotifyPropertyChanged("LatestCommits");
                }
            }
        }

        private SettingsViewModel _settings = new SettingsViewModel();
        public SettingsViewModel Settings
        {
            get
            {
                return _settings;
            }
            set
            {
                if (value != _settings)
                {
                    _settings = value;
                    NotifyPropertyChanged("Settingss");
                }
            }
        }

        public void LoadData()
        {
            _topCommitters.LoadData();
            _buildStatus.LoadData();
            _workingDaysLeft.LoadData();
            _latestCommits.LoadData();
        }
    }
}
