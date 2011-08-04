using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Phone.Controls;
using Smeedee.WP7.ViewModels.Settings;
using Smeedee.WP7.ViewModels.Widgets;
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<PivotItem> OnScreenWidgets { get; private set; }
        public bool IsDataLoaded;

        public MainViewModel()
        {
            OnScreenWidgets = new ObservableCollection<PivotItem>();
            foreach (var widget in FindAvailableWidgets())
                if (WidgetIsEnabled(widget))
                    OnScreenWidgets.Add(widget.View);
        }

        public IEnumerable<IWpWidget> FindAvailableWidgets()
        {
            return new List<IWpWidget>
                       {
                           new BuildStatusWidget(),
                           new HomeScreenWidget(),
                           new LatestCommitsWidget(),
                           new SettingsWidget(),
                           new TopCommittersWidget(),
                           new WorkingDaysLeftWidget()
                       };
        }

        public bool WidgetIsEnabled(IWpWidget widget)
        {
            return true;
        }

        public void LoadData()
        {
        }
    }
}
