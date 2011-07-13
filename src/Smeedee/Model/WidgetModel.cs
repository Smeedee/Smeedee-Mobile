using System;
using Smeedee;

namespace Smeedee.Model
{
    public class WidgetModel
    {
        public string Name { get; private set; }
        public string DescriptionStatic { get; private set; }
        public Type Type { get; private set; }

        public WidgetModel(string name, string descriptionStatic, Type type)
        {
            Guard.NotNull(name, type);
            Name = name;
            DescriptionStatic = descriptionStatic;
            Type = type;
        }
    }
}