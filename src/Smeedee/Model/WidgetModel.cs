using System;
using Smeedee;

namespace Smeedee.Model
{
    public class WidgetModel
    {
        public string Name { get; private set; }
        public string StaticDescription { get; private set; }
        public Type Type { get; private set; }
        public Type SettingsType { get; set; }
		
		private IPersistenceService persistence = SmeedeeApp.Instance.ServiceLocator.Get<IPersistenceService>();

        public WidgetModel(string name, string staticDescription, Type type)
        {
            Guard.NotNull(name, type);
            Name = name;
            StaticDescription = staticDescription;
            Type = type;
        }
		
		public bool Enabled
		{
			get { return persistence.Get(Name, false); }
			set { persistence.Save(Name, value); }
		}
    }
}