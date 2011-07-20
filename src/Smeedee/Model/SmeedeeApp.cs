using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Smeedee.Model
{
    public class SmeedeeApp
    {
        public readonly static SmeedeeApp Instance = new SmeedeeApp();

        // This class is a singleton: private constructor; static instance variable.
        private SmeedeeApp()
        {
            ServiceLocator = new ServiceLocator();
            AvailableWidgets = new List<WidgetModel>();
        }

        public ServiceLocator ServiceLocator { get; private set; }
		
        public IList<WidgetModel> AvailableWidgets { get; private set; }
		public IEnumerable<WidgetModel> EnabledWidgets 
		{ 
			get
			{
				var persistence = ServiceLocator.Get<IPersistenceService>();
				foreach (var model in AvailableWidgets) {
					if (persistence.Get(model.Name, false))
						yield return model;
				}
			}
		}

        public void RegisterAvailableWidgets()
        {
            var types = Assembly.GetCallingAssembly().GetTypes();
            var widgetsAsType = from type in types
                          where typeof(IWidget).IsAssignableFrom(type) && !type.IsInterface
                          select type;
            
            foreach (var widgetAsType in widgetsAsType)
            {
                if (WidgetTypeIsAlreadyRegistered(widgetAsType)) continue;
                AvailableWidgets.Add(GetModelFromType(widgetAsType));
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
                throw new ArgumentException("A widget was passed without class attributes");

            var model = ModelFromAttributes(((WidgetAttribute[])widgetAttributes)[0], type);
            return model;
        }

        private static WidgetModel ModelFromAttributes(WidgetAttribute attr, Type type)
        {
            return new WidgetModel(attr.Name, attr.StaticDescription, type);
        }
    }
}
