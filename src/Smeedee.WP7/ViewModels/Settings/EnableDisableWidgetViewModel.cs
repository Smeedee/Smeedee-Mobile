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

namespace Smeedee.WP7.ViewModels.Settings
{
    public class EnableDisableWidgetViewModel : ViewModelBase
    {
        private IPersistenceService _persistance;
        public EnableDisableWidgetViewModel(IPersistenceService persistance)
        {
            _persistance = persistance;
        }

        public bool Enabled
        {
            get
            {
                return _persistance.Get("WidgetEnabled_"+WidgetName, true);
            }
            set
            {
                if (value != _persistance.Get("WidgetEnabled_"+WidgetName, true))
                {
                    _persistance.Save("WidgetEnabled_" + WidgetName, value);
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
