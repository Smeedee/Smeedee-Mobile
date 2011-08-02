using System;
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

        public void LoadData()
        {
            _topCommitters.LoadData();
            _buildStatus.LoadData();
        }
    }
}
