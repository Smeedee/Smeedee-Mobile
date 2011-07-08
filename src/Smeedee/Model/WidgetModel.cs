using System;
using Smeedee.Utilities;

namespace Smeedee.Model
{
    public class WidgetModel
    {
        public string Name { get; private set; }
        public int Icon { get; private set; }
        public string DescriptionStatic { get; private set; }
        public Type Type { get; private set; }
        public bool IsEnabled { get; set; }

        public WidgetModel(string name, int icon, string descriptionStatic, Type type, bool isEnabled)
        {
            Guard.NotNull(name, type, descriptionStatic);
            Name = name;
            Icon = icon;
            DescriptionStatic = descriptionStatic;
            Type = type;
            IsEnabled = isEnabled;
        }
    }
}