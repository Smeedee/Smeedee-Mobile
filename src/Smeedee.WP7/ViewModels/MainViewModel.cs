using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Settings;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<PivotItem> WidgetViews { get; private set; }
        public ObservableCollection<PivotItem> SettingsViews { get; private set; }
        private Dictionary<PivotItem, IWpWidget> viewToWidgetMap;
        private SmeedeeApp _app = SmeedeeApp.Instance;

        public MainViewModel()
        {
            FindAvailableWidgets();
            WidgetsAreShowing = true;
        }

        private void FindAvailableWidgets()
        {
            _app.RegisterAvailableWidgets();
            var widgetModels = _app.AvailableWidgets; //PretendToFindModels();
            var widgets = widgetModels.Select(m => Activator.CreateInstance(m.Type) as IWpWidget);

            viewToWidgetMap = new Dictionary<PivotItem, IWpWidget>();
            WidgetViews = new ObservableCollection<PivotItem>();
            foreach (var widget in widgets)
            {
                WidgetViews.Add(widget.View);
                viewToWidgetMap.Add(widget.View, widget);
            }

            var enableDisableWidget = new SettingsWidget(widgetModels);
            SettingsViews = new ObservableCollection<PivotItem> { enableDisableWidget.View };
        }
        
        private bool WidgetIsEnabled(IWpWidget widget)
        {
            return true; //TODO
        }
        
        public IWpWidget GetWidgetForView(PivotItem view)
        {
            return viewToWidgetMap[view];
        }

        private bool _settingsAreShowing;
        public bool SettingsAreShowing
        {
            get
            {
                return _settingsAreShowing;
            }
            set
            {
                if (value != _settingsAreShowing)
                {
                    _settingsAreShowing = value;
                    NotifyPropertyChanged("SettingsAreShowing");
                    NotifyPropertyChanged("WidgetsAreShowing");
                }
            }
        }

        public bool WidgetsAreShowing
        {
            get
            {
                return !_settingsAreShowing;
            }
            set
            {
                if (value != !_settingsAreShowing)
                {
                    _settingsAreShowing = !value;
                    NotifyPropertyChanged("SettingsAreShowing");
                    NotifyPropertyChanged("WidgetsAreShowing");
                }
            }
        }
    }
}
