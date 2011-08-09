using System;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.Views;

namespace Smeedee.WP7.Widgets
{
    [Widget(Name)]
    public class HomeScreenWidget : IWpWidget
    {
        public const string Name = "Smeedee";

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
