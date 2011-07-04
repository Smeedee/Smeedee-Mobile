using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class SmeedeeApp
    {
        // This class is a singleton: private constructor; static instance variable.
        private SmeedeeApp()
        {
            AvailableWidgetTypes = new List<Type>();
        }
        public readonly static SmeedeeApp Instance = new SmeedeeApp();
        
        // The data service, responsible for connecting to the Smeedee back-end.
        // Overrideable, for faking and testing purposes.
        public static ISmeedeeService SmeedeeService = new SmeedeeHttpService();
        
        public List<Type> AvailableWidgetTypes { get; private set; }

        public void RegisterAvailableWidgets()
        {
            var types = Assembly.GetCallingAssembly().GetTypes();
            var widgets = from type in types where typeof(IWidget).IsAssignableFrom(type) select type;
            var concrete = from type in widgets where !(type.IsAbstract || type.IsInterface) select type;
            RegisterAvailableWidgets(concrete);
        }
        
        public void RegisterAvailableWidgets(IEnumerable<Type> widgetTypes)
        {
            foreach (var widget in widgetTypes)
            {
                if (AvailableWidgetTypes.Where(e => e.GUID == widget.GUID).Count() > 0) continue;
                AvailableWidgetTypes.Add(widget);
            }
        }

    }
}
