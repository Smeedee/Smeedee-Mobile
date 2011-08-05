using System;
using System.Collections.Generic;
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
        public SettingsViewModel ViewModel { get; set; }

        public SettingsWidget(IEnumerable<WidgetModel> widgets)
        {
            ViewModel = new SettingsViewModel(widgets);
            View = new SettingsView { DataContext = ViewModel };
            
        }
    }
}
