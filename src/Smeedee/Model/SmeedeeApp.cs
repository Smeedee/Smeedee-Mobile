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
            AvailableWidgetTypes = new List<WidgetModel>();
            ServiceLocator = new ServiceLocator();
        }

        public ServiceLocator ServiceLocator { get; private set; }
        public List<WidgetModel> AvailableWidgetTypes { get; private set; }

        private WidgetModel GetModelFromType(Type type)
        {
            object[] widgetAttributes = type.GetCustomAttributes(typeof(WidgetAttribute), true);

            bool typeHasAttributes = (widgetAttributes.Count() > 0 && widgetAttributes is WidgetAttribute[]);
            if (!typeHasAttributes)
                throw new ArgumentException("A widget without attributes was passed");

            var model = ModelFromAttributes(((WidgetAttribute[])widgetAttributes)[0]);
            model.Type = type;

            // TODO: Write code for checking whether or not the widget is enabled.
            return model;
        }

        public void RegisterAvailableWidgets()
        {
            var types = Assembly.GetCallingAssembly().GetTypes();
            var widgets = from type in types
                          where typeof(IWidget).IsAssignableFrom(type) &&
                                !type.IsInterface
                          select type;
            
            foreach (var widget in widgets)
            {
                if (WidgetTypeIsAlreadyRegistered(widget)) continue;
                AvailableWidgetTypes.Add(GetModelFromType(widget));
            }
        }

        private static WidgetModel ModelFromAttributes(WidgetAttribute attr)
        {
            return new WidgetModel
            {
                Name = attr.Name,
                Icon = attr.Icon
            };
        }

        private bool WidgetTypeIsAlreadyRegistered(Type widgetType)
        {
            foreach (var registeredWidgetType in AvailableWidgetTypes)
            {
                if (registeredWidgetType.Type.Name == widgetType.Name) return true;
            }
            
            return false;
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
