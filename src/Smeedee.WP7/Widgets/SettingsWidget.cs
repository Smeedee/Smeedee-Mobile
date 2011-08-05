using System;
using System.Collections.Generic;
using System.Linq;
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
using Smeedee.WP7.ViewModels.Settings;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    public class SettingsWidget
    {
        public PivotItem View { get; set; }
        private SettingsViewModel _viewModel;

        public SettingsWidget()
        {
            _viewModel = new SettingsViewModel();
            View = new SettingsView { DataContext = _viewModel };
        }

        public IEnumerable<WidgetModel> EnabledWidgets()
        {
            return SmeedeeApp.Instance.AvailableWidgets
                .Where(m => _viewModel.Widgets.Where(vm => vm.WidgetName == m.Name).First().Enabled);
        }
    }
}
