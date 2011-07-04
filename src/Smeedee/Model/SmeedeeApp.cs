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
		private SmeedeeApp() { }
		public readonly static SmeedeeApp Instance = new SmeedeeApp();
		
		// The data service, responsible for connecting to the Smeedee back-end.
		// Overrideable, for faking and testing purposes.
		public static ISmeedeeService SmeedeeService = new SmeedeeHttpService();
		
		private List<Type> availableWidgetTypes = new List<Type>();
		
		public void RegisterAvailableWidgets()
		{
            var types = Assembly.GetCallingAssembly().GetTypes();
            var widgets = from type in types where typeof(IWidget).IsAssignableFrom(type) select type;
            var concrete = from type in widgets where !(type.IsAbstract || type.IsInterface) select type;
            availableWidgetTypes.AddRange(concrete);
		}
		
		public void RegisterAvailableWidgets(IEnumerable<Type> widgetTypes)
		{
			foreach (var widget in widgetTypes)
			{
				if (availableWidgetTypes.Contains(widget)) continue;
				availableWidgetTypes.Add(widget);
			}
		}
		
		public void ClearRegisteredWidgets()
		{
			availableWidgetTypes.Clear();
		}
		
		public IEnumerable<IWidget> GetWidgets()
		{
			foreach (var widgetType in availableWidgetTypes)
			{
				yield return Activator.CreateInstance(widgetType) as IWidget;
			}
		}
	}
}
