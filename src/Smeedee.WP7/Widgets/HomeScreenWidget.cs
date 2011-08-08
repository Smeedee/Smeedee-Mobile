using System;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    [Widget("Smeedee")]
    public class HomeScreenWidget : IWpWidget
    {
        public HomeScreenWidget()
        {
            View = new HomeScreenView();
        }
        public PivotItem View { get; set; }

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
