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
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    [Widget("Working Days Left", StaticDescription = "Actual working days left")]
    public class WorkingDaysLeftWidget : IWpWidget
    {
        private WorkingDaysLeftViewModel _workingDaysLeftViewModel;

        public WorkingDaysLeftWidget()
        {
            _workingDaysLeftViewModel = new WorkingDaysLeftViewModel();
            View = new WorkingDaysLeftView() { DataContext = _workingDaysLeftViewModel };
            Refresh();
        }

        public PivotItem View { get; set; }

        public void Refresh()
        {
            _lastRefreshTime = DateTime.Now;
            _workingDaysLeftViewModel.LoadData();
        }

        private DateTime _lastRefreshTime;
        public DateTime LastRefreshTime()
        {
            return DateTime.Now;
        }

        public string GetDynamicDescription()
        {
            return "";
        }

        public event EventHandler DescriptionChanged;

    }
}
