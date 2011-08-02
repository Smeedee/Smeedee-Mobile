using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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
                DaysLeftSuffix = CreateSuffix();
            }));
            IsDataLoaded = true;
        }

        public void Refresh()
        {
            LoadData();
        }

        public string CreateSuffix()
        {
            return (DaysLeft == 1) ? "working day left" : "working days left";
        }

        private DateTime lastRefreshTime;
        private string dynamicDescription;

        public DateTime LastRefreshTime()
        {
            return lastRefreshTime;
        }

        public string GetDynamicDescription()
        {
            return dynamicDescription;
        }

        public event EventHandler DescriptionChanged;
    }
}
