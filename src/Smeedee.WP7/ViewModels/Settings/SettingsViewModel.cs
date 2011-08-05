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
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Widgets;

namespace Smeedee.WP7.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        private IPersistenceService _persistance;

        public SettingsViewModel()
        {
            _persistance = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
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
                           new EnableDisableWidgetViewModel(_persistance) {WidgetName = "Top Committers"},
                           new EnableDisableWidgetViewModel(_persistance) {WidgetName = "Build Status"},
                           new EnableDisableWidgetViewModel(_persistance) {WidgetName = "Working Days Left"},
                           new EnableDisableWidgetViewModel(_persistance) {WidgetName = "Latest Commits"}
                       };
        }

        public ObservableCollection<EnableDisableWidgetViewModel> Widgets { get; private set; }
        public void Show()
        {
            Console.WriteLine("dhh");
        }
    }
}
