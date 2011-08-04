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
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    public class HomeScreenWidget : IWpWidget
    {
        public HomeScreenWidget()
        {
            View = new HomeScreenView();
        }
        public FrameworkElement View { get; set; }

        public void Refresh()
        {
        }

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
