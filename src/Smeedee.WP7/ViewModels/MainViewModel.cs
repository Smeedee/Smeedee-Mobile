using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
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
        private readonly Dictionary<PivotItem, IWpWidget> viewToWidgetMap;
        private SettingsWidget _enableDisableWidget;
        private SmeedeeApp _app = SmeedeeApp.Instance;

        public MainViewModel()
        {
            _app.RegisterAvailableWidgets();
            WidgetViews = new ObservableCollection<PivotItem>();
            viewToWidgetMap = new Dictionary<PivotItem, IWpWidget>();
            InstantiateSettings();
            InstantiateWidgets();
            WidgetsAreShowing = true;
        }


        private void InstantiateSettings()
        {
            _enableDisableWidget = new SettingsWidget();
            SettingsViews = new ObservableCollection<PivotItem> { _enableDisableWidget.View };
        }

        private void InstantiateWidgets()
        {
            var modelsToBeEnabled = _enableDisableWidget.EnabledWidgets();
            var enabledWidgets = viewToWidgetMap.Values;
            var enabledWidgetsThatShouldBeDisabled =
                enabledWidgets.Where(enabledWidget => !modelsToBeEnabled.Any(m => m.Type == enabledWidget.GetType())).ToList();
            var disabledModelsThatShouldBeEnabled =
                modelsToBeEnabled.Where(model => !enabledWidgets.Any(w => w.GetType() == model.Type)).ToList();

            foreach (var widget in enabledWidgetsThatShouldBeDisabled)
                RemoveWidget(widget);
            foreach (var model in disabledModelsThatShouldBeEnabled)
                AddWidget(model);
        }

        private void AddWidget(WidgetModel model)
        {
            var widget = Activator.CreateInstance(model.Type) as IWpWidget;
            WidgetViews.Add(widget.View);
            viewToWidgetMap.Add(widget.View, widget);
        }

        private void RemoveWidget(IWpWidget widget)
        {
            WidgetViews.Remove(widget.View);
            viewToWidgetMap.Remove(widget.View);
        }

        private void OnExitSettings()
        {
            InstantiateWidgets();
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
                    if (!_settingsAreShowing) OnExitSettings();
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
                    if (!_settingsAreShowing) OnExitSettings();
                    NotifyPropertyChanged("SettingsAreShowing");
                    NotifyPropertyChanged("WidgetsAreShowing");
                }
            }
        }
    }
}
