using System;
using System.Windows;
using Smeedee.Model;

namespace Smeedee.WP7.ViewModels.Widgets
{
    public class WorkingDaysLeftViewModel : ViewModelBase, IWidget
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

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        public void LoadData()
        {
            model.Load(() => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                DaysLeft = model.DaysLeft;
                DaysLeftSuffix = model.DaysLeftText;
                UntillText = model.UntillText;
            }));
            IsDataLoaded = true;
        }

        public void Refresh()
        {
            LoadData();
            if (model.LoadError)
            {
                DaysLeft = 0;
                DaysLeftSuffix = "";
                UntillText = "Failed to load project info from server";
                return;
            }
            _lastRefreshTime = DateTime.Now;
        }
        private DateTime _lastRefreshTime;
        public DateTime LastRefreshTime()
        {
            return _lastRefreshTime;
        }

        private string dynamicDescription;
        public string GetDynamicDescription()
        {
            return dynamicDescription;
        }
        public event EventHandler DescriptionChanged;
    }
}
