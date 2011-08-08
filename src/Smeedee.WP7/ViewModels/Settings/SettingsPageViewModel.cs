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
    public class SettingsPageViewModel : ViewModelBase
    {
        private IPersistenceService _persistance;

        public ObservableCollection<EnableDisableWidgetItemViewModel> EnableDisableWidgets { get; private set; }

        public LoginViewModel LoginViewModel { get; private set; }

        public SettingsPageViewModel()
        {
            LoginViewModel = new LoginViewModel();
            _persistance = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
            EnableDisableWidgets = new ObservableCollection<EnableDisableWidgetItemViewModel>();
            foreach (var model in SmeedeeApp.Instance.AvailableWidgets)
                EnableDisableWidgets.Add(new EnableDisableWidgetItemViewModel(_persistance) { WidgetName = model.Name});
        }
    }
}
