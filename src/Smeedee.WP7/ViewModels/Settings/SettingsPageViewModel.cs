using System.Collections.ObjectModel;

using Smeedee.Model;

namespace Smeedee.WP7.ViewModels.Settings
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private IPersistenceService _persistance;

        public ObservableCollection<EnableDisableWidgetItemViewModel> EnableDisableWidgets { get; private set; }

        public LoginViewModel LoginViewModel { get; private set; }

        public SettingsPageViewModel()
        {
            LoginViewModel = new LoginViewModel();
            _persistance = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
            EnableDisableWidgets = new ObservableCollection<EnableDisableWidgetItemViewModel>();
            foreach (var model in SmeedeeApp.Instance.AvailableWidgets)
                EnableDisableWidgets.Add(new EnableDisableWidgetItemViewModel(_persistance) { WidgetName = model.Name});
        }
    }
}
