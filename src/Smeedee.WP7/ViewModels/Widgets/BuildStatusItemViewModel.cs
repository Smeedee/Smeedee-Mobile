using System;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class BuildStatusItemViewModel : ViewModelBase
    {
        private string _projectName;
        public string ProjectName
        {
            get
            {
                return _projectName;
            }
            set
            {
                if (value != _projectName)
                {
                    _projectName = value;
                    NotifyPropertyChanged("ProjectName");
                }
            }
        }
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (value != _userName)
                {
                    _userName = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }
        private string _buildTime;
        public string BuildTime
        {
            get
            {
                return _buildTime;
            }
            set
            {
                if (value != _buildTime)
                {
                    _buildTime = value;
                    NotifyPropertyChanged("BuildTime");
                }
            }
        }
    }
}
