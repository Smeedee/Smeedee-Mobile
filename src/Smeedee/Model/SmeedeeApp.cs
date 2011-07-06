using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class SmeedeeApp
    {
        public readonly static SmeedeeApp Instance = new SmeedeeApp();

        // This class is a singleton: private constructor; static instance variable.
        private SmeedeeApp()
        {
            AvailableWidgets = new List<WidgetModel>();
            ServiceLocator = new ServiceLocator();
        }

        public ServiceLocator ServiceLocator { get; private set; }
        public List<WidgetModel> AvailableWidgets { get; private set; }

        public void RegisterAvailableWidgets()
        {
            var types = Assembly.GetCallingAssembly().GetTypes();
            var widgets = from type in types
                          where typeof(IWidget).IsAssignableFrom(type) && !type.IsInterface
                          select type;
            
            foreach (var widget in widgets)
            {
                if (WidgetTypeIsAlreadyRegistered(widget)) continue;
                AvailableWidgets.Add(GetModelFromType(widget));
            }
        }

        private bool WidgetTypeIsAlreadyRegistered(Type widgetType)
        {
            foreach (var registeredWidget in AvailableWidgets)
            {
                if (registeredWidget.Type.Name == widgetType.Name) return true;
            }
            
            return false;
        }

        private WidgetModel GetModelFromType(Type type)
        {
            var widgetAttributes = type.GetCustomAttributes(typeof(WidgetAttribute), true);
            var typeHasAttributes = (widgetAttributes.Count() > 0 && widgetAttributes is WidgetAttribute[]);
            if (!typeHasAttributes)
                throw new ArgumentException("A widget without attributes was passed");

            var model = ModelFromAttributes(((WidgetAttribute[])widgetAttributes)[0]);
            return model;
        }

        private static WidgetModel ModelFromAttributes(WidgetAttribute attr)
        {
            return new WidgetModel(attr.Name, attr.Icon, attr.Type, attr.IsEnabled);
        }

        public string GetStoredLoginKey()
        {
            return "myPassword";
        }

        public string GetStoredLoginUrl()
        {
            return "http://my.url";
        }
    }
}
