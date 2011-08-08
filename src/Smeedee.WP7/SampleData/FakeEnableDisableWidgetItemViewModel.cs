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
using Smeedee.WP7.ViewModels.Settings;

namespace Smeedee.WP7.SampleData
{
    public class FakeEnableDisableWidgetItemViewModel : EnableDisableWidgetItemViewModel
    {
        public FakeEnableDisableWidgetItemViewModel() : base(null)
        {
        }

        private bool _enabled;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }
            set
            {
                if (value != _enabled)
                {
                    _enabled = value;
                    NotifyPropertyChanged("Enabled");
                }
            }
        }

        private string _widgetName;
        public string WidgetName
        {
            get
            {
                return _widgetName;
            }
            set
            {
                if (value != _widgetName)
                {
                    _widgetName = value;
                    NotifyPropertyChanged("WidgetName");
                }
            }
        }
    }
}
