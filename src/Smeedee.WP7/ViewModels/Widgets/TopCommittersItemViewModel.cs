
using System;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class TopCommittersItemViewModel : ViewModelBase
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (value != _name)
                {
                    _name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        private string _commits;
        public string Commits
        {
            get
            {
                return _commits;
            }
            set
            {
                if (value != _commits)
                {
                    _commits = value;
                    NotifyPropertyChanged("Commits");
                }
            }
        }

        private string _commitBarWidth;
        public string CommitBarWidth
        {
            get
            {
                return _commitBarWidth;
            }
            set
            {
                if (value != _commitBarWidth)
                {
                    _commitBarWidth = value;
                    NotifyPropertyChanged("CommitBarWidth");
                }
            }
        }
        private Uri _image;
        public Uri Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (value != _image)
                {
                    _image = value;
                    NotifyPropertyChanged("Image");
                }
            }
        }
    }
}