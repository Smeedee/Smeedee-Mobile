using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Settings;
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<PivotItem> WidgetViews { get; private set; }
        public ObservableCollection<PivotItem> SettingsViews { get; private set; }
        public LoginViewModel LoginViewModel { get; private set; }
        private readonly Dictionary<PivotItem, IWpWidget> viewToWidgetMap;
        private SmeedeeApp _app = SmeedeeApp.Instance;

        public MainViewModel()
        {
            LoginViewModel = new LoginViewModel();
            _app.RegisterAvailableWidgets();
            WidgetViews = new ObservableCollection<PivotItem>();
            viewToWidgetMap = new Dictionary<PivotItem, IWpWidget>();
            InstantiateWidgets();
            WidgetsAreShowing = true;
        }

        private void InstantiateWidgets()
        {
            var modelsToBeEnabled = SmeedeeApp.Instance.AvailableWidgets.Where(EnableDisableWidgetItemViewModel.IsEnabled);
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
            //TODO: This sometimes throws an ArgumentOutOfRangeException (from within the Pivot view).
            //We need to figure out when and why and stop it, or see if we can catch it and ignore it
            WidgetViews.Remove(widget.View);
            viewToWidgetMap.Remove(widget.View);
        }

        public void OnExitSettings()
        {
            InstantiateWidgets();
        }

        public IWpWidget GetWidgetForView(PivotItem view)
        {
            return viewToWidgetMap[view];
        }

        private bool _widgetsAreShowing;
        public bool WidgetsAreShowing
        {
            get
            {
                return _widgetsAreShowing;
            }
            set
            {
                if (value != _widgetsAreShowing)
                {
                    _widgetsAreShowing = value;
                    NotifyPropertyChanged("WidgetsAreShowing");
                }
            }
        }

        public void LoadWidgets()
        {
            foreach (var widget in viewToWidgetMap.Values)
                widget.Refresh();
        }
    }
}
