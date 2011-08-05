using System;
using System.Windows;
using Smeedee.Model;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class WorkingDaysLeftViewModel : ViewModelBase
    {
        private readonly WorkingDaysLeft model;

        public WorkingDaysLeftViewModel()
        {
            model = new WorkingDaysLeft();
        }

        private int _daysLeft;
        public int DaysLeft
        {
            get
            {
                return _daysLeft;
            }
            set
            {
                if (value != _daysLeft)
                {
                    _daysLeft = value;
                    NotifyPropertyChanged("DaysLeft");
                }
            }
        }

        private string _daysLeftSuffix;
        public string DaysLeftSuffix
        {
            get
            {
                return _daysLeftSuffix;
            }
            set
            {
                if (value != _daysLeftSuffix)
                {
                    _daysLeftSuffix = value;
                    NotifyPropertyChanged("DaysLeftSuffix");
                }
            }
        }
        private string _untillText;
        public string UntillText
        {
            get
            {
                return _untillText;
            }
            set
            {
                if (value != _untillText)
                {
                    _untillText = value;
                    NotifyPropertyChanged("UntillText");
                }
            }
        }
        
        public void LoadData()
        {
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                if (model.LoadError)
                {
                    DaysLeft = 0;
                    DaysLeftSuffix = "";
                    UntillText = "Failed to load project info from server";
                } else
                {
                    DaysLeft = model.DaysLeft;
                    DaysLeftSuffix = model.DaysLeftText;
                    UntillText = model.UntillText;
                }
            }));
        }
    }
}
