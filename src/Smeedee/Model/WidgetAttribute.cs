using System;
using Smeedee.Utilities;


namespace Smeedee.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetAttribute : Attribute
    {
        public string Name { get; private set; }
        public int Icon { get; private set; }
        public string DescriptionStatic { get; set; }
        public bool IsEnabled { get; set; } //TODO: Discuss private/public on this attribute

        public WidgetAttribute(string name, int icon)
        {
            Guard.NotNull(name);
            Name = name;
            Icon = icon;
        }
    }
}
