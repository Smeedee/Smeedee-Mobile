using Smeedee.Model;

namespace Smeedee.WP7.ViewModels.Settings
{
    public class EnableDisableWidgetItemViewModel : ViewModelBase
    {
        private const string _enabledPrefix = "WidgetEnabled_";

        public static bool IsEnabled(WidgetModel model)
        {
            var persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();
            return persistence.Get(_enabledPrefix + model.Name, true);
        }

        private IPersistenceService _persistance;
        
        public EnableDisableWidgetItemViewModel(IPersistenceService persistance)
        {
            _persistance = persistance;
        }

        public bool Enabled
        {
            get
            {
                return _persistance.Get(_enabledPrefix+WidgetName, true);
            }
            set
            {
                if (value != _persistance.Get(_enabledPrefix+WidgetName, true))
                {
                    _persistance.Save(_enabledPrefix + WidgetName, value);
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
