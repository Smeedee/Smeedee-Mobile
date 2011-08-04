using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using Microsoft.Phone.Controls;
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

        public bool IsDataLoaded;

        public MainViewModel()
        {
            FindAvailableWidgets();
            WidgetsAreShowing = true;
        }

        private void FindAvailableWidgets()
        {
            var widgets = new IWpWidget[] {
                                  new BuildStatusWidget(),
                                  new HomeScreenWidget(),
                                  new LatestCommitsWidget(),
                                  new TopCommittersWidget(),
                                  new WorkingDaysLeftWidget()
                                };
            var settings = new[] {
                                  new SettingsWidget()
                                 };

            viewToWidgetMap = new Dictionary<PivotItem, IWpWidget>();
            WidgetViews = new ObservableCollection<PivotItem>();
            foreach (var widget in widgets)
            {
                WidgetViews.Add(widget.View);
                viewToWidgetMap.Add(widget.View, widget);
            }

            SettingsViews = new ObservableCollection<PivotItem> { new SettingsWidget().View };
            foreach (var widget in settings)
            {
                SettingsViews.Add(widget.View);
                viewToWidgetMap.Add(widget.View, widget);
            }
        }

        public bool WidgetIsEnabled(IWpWidget widget)
        {
            return true; //TODO
        }

        public void LoadData()
        { //TODO: Remove this
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
