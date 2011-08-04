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
using Smeedee.WP7.ViewModels.Settings;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    public class SettingsWidget : IWpWidget
    {
        public PivotItem View { get; set; }

        public SettingsWidget()
        {
            View = new SettingsView { DataContext = new SettingsViewModel() };
        }

        public void Refresh()
        {
        }

        public DateTime LastRefreshTime()
        {
            throw new NotImplementedException();
        }

        public string GetDynamicDescription()
        {
            throw new NotImplementedException();
        }

        public event EventHandler DescriptionChanged;

    }
}
