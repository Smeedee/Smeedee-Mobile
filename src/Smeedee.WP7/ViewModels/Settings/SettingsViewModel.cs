using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Smeedee.WP7.ViewModels.Widgets;

namespace Smeedee.WP7.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel()
        {
            Widgets = new ObservableCollection<EnableDisableWidgetViewModel>();
            FindAvailableWidgets();
        }

        private void FindAvailableWidgets()
        {
            foreach (var widget in FakeData()) 
                Widgets.Add(widget);
        }

        private List<EnableDisableWidgetViewModel> FakeData()
        {
            return new List<EnableDisableWidgetViewModel>
                       {
                           new EnableDisableWidgetViewModel {Enabled = true, WidgetName = "Top Committers"},
                           new EnableDisableWidgetViewModel {Enabled = true, WidgetName = "Build Status"},
                           new EnableDisableWidgetViewModel {Enabled = true, WidgetName = "Working Days Left"},
                           new EnableDisableWidgetViewModel {Enabled = true, WidgetName = "Latest Commits"}
                       };
        }

        public ObservableCollection<EnableDisableWidgetViewModel> Widgets { get; private set; }
        public void Show()
        {
            Console.WriteLine("dhh");
        }
    }
}
