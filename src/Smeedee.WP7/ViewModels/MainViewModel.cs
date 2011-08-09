using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using Smeedee.Model;
using Smeedee.WP7.ViewModels.Settings;
using Smeedee.WP7.Widgets;

namespace Smeedee.WP7.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public ObservableCollection<PivotItem> SettingsViews { get; private set; }
        public LoginViewModel LoginViewModel { get; private set; }
        private readonly Dictionary<PivotItem, IWpWidget> viewToWidgetMap;
        private SmeedeeApp _app = SmeedeeApp.Instance;
        public MainPage MainPage;

        public MainViewModel()
        {
            LoginViewModel = new LoginViewModel();
            viewToWidgetMap = new Dictionary<PivotItem, IWpWidget>();
        }

        /* Databinding the items in a Pivot control is seriously bugged (as of the first WP7 release, at least)
         * We experienced the problem documented here:
         * http://social.msdn.microsoft.com/Forums/en-US/windowsphone7series/thread/331e53f2-c169-41b9-a946-c5ee5980562a
         * 
         * The solution is to not databind, but rather recreate the entire Pivot control when we change its content.
         * We do not currently recycle views between each recreation, so we reload every widget when we change 
         * which widgets are enabled and disabled.         
         */
        public void RecreatePivotControlIfChanged()
        {
            if (EnabledWidgetsHasChanged())
                CreatePivotControl();
        }

        private IEnumerable<WidgetModel> GetModelsToBeShown()
        {
            var homeScreen = _app.AvailableWidgets.Where(m => m.Name == HomeScreenWidget.Name).First();
            var availableWidgets = _app.AvailableWidgets.Where(m => m.Name != HomeScreenWidget.Name);
            var modelsToBeEnabled = availableWidgets.Where(EnableDisableWidgetItemViewModel.IsEnabled);
            return modelsToBeEnabled.Count() == 0 ? new[] { homeScreen } : modelsToBeEnabled;
        }
        

        private bool EnabledWidgetsHasChanged()
        {
            var current = viewToWidgetMap.Values;
            var shouldBeEnabled = GetModelsToBeShown();

            if (current.Count != shouldBeEnabled.Count()) return true;
            foreach (var widgetModel in shouldBeEnabled)
            {
                if (!current.Any(widget => widgetModel.Type == widget.GetType()))
                    return true;
            }
            return false;
        }

        private Pivot prevPivot;
        private void CreatePivotControl()
        {
            viewToWidgetMap.Clear();
            var toRemove = prevPivot ?? MainPage.WidgetsPivot;
            var pivot = CreatePivot();
            prevPivot = pivot;
            foreach (var model in GetModelsToBeShown())
            {
                var widget = Activator.CreateInstance(model.Type) as IWpWidget;
                if (widget == null) continue; 
                pivot.Items.Add(widget.View);
                viewToWidgetMap.Add(widget.View, widget);
            }
            MainPage.LayoutRoot.Children.Remove(toRemove);
            MainPage.LayoutRoot.Children.Add(pivot);
        }

        private static Pivot CreatePivot()
        {
            var pivot = new Pivot() {Foreground = new SolidColorBrush(Color.FromArgb(0xff, 0xf2, 0x50, 0x00))};
            pivot.Title = new TextBlock() {Foreground = new SolidColorBrush(Colors.White), Text = "SMEEDEE"};
            return pivot;
        }
        
        public void OnExitSettings()
        {
            RecreatePivotControlIfChanged();
        }

        public IWpWidget GetWidgetForView(PivotItem view)
        {
            return viewToWidgetMap[view];
        }
        
        private bool _loginIsVisible;
        public bool LoginIsVisible
        {
            get
            {
                return _loginIsVisible;
            }
            set
            {
                if (value != _loginIsVisible)
                {
                    _loginIsVisible = value;
                    NotifyPropertyChanged("LoginIsVisible");
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
