using System;
using Smeedee.Services;

namespace Smeedee.Model
{
    public class WidgetModel
    {
        public string Name { get; private set; }
        public int Icon { get; private set; }
        public string DescriptionStatic { get; private set; }
        public Type Type { get; private set; }

        public WidgetModel(string name, int icon, string descriptionStatic, Type type)
        {
            Guard.NotNull(name, type, descriptionStatic);
            Name = name;
            Icon = icon;
            DescriptionStatic = descriptionStatic;
            Type = type;
        }
    }
}