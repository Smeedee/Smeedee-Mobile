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
        public ObservableCollection<PivotItem> OnScreenWidgets = new ObservableCollection<PivotItem>();  
        public bool IsDataLoaded;

        public MainViewModel()
        {
            foreach (var widget in FindAvailableWidgets())
                OnScreenWidgets.Add(widget.View);
        }

        public IEnumerable<IWpWidget> FindAvailableWidgets()
        {
            return new List<IWpWidget>();
        }

        public void LoadData()
        {
        }
    }
}
