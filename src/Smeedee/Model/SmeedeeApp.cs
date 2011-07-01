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
		public static SmeedeeApp Instance = new SmeedeeApp();
		
		// The data service, responsible for connecting to the Smeedee back-end.
		// Overrideable, for faking and testing purposes.
		public static ISmeedeeService SmeedeeService = new SmeedeeHttpService();
		
		private List<Type> availableWidgetTypes = new List<Type>();
		
		public void RegisterAvailableWidgets()
		{
			var types = Assembly.GetCallingAssembly().GetTypes();
			
			foreach (var type in types)
			{
				if (typeof(IWidget).IsAssignableFrom(type))
				{
					availableWidgetTypes.Add(type);
				}
			}
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
