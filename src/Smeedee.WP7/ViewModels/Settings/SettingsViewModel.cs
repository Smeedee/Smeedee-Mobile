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
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7.ViewModels.Settings
{
    public class SettingsViewModel : ViewModelBase
    {
        private IPersistenceService _persistance;

        public ObservableCollection<EnableDisableWidgetViewModel> Widgets { get; private set; }

        public SettingsViewModel()
        {
            _persistance = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
            Widgets = new ObservableCollection<EnableDisableWidgetViewModel>();
            foreach (var model in SmeedeeApp.Instance.AvailableWidgets)
                Widgets.Add(new EnableDisableWidgetViewModel(_persistance) { WidgetName = model.Name});
        }
    }
}
