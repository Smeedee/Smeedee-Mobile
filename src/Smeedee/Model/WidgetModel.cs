using System;

namespace Smeedee.Model
{
    public class WidgetModel
    {
        public string Name { get; private set; }
        public string StaticDescription { get; private set; }
        public Type Type { get; private set; }

        public WidgetModel(string name, string staticDescription, Type type)
        {
            Guard.NotNull(name, type);
            Name = name;
            StaticDescription = staticDescription;
            Type = type;
        }
    }
}