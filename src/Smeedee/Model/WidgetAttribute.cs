using System;
using Smeedee;


namespace Smeedee.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetAttribute : Attribute
    {
        public string Name { get; private set; }
        public int Icon { get; private set; }
        public string DescriptionStatic { get; set; }

        public WidgetAttribute(string name, int icon)
        {
            Guard.NotNull(name, icon);
            Name = name;
            Icon = icon;
        }
    }
}
