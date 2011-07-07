using System;


namespace Smeedee.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetAttribute : Attribute
    {
        public string Name { get; private set; }
        public int Icon { get; private set; }
        public bool IsEnabled { get; set; } //TODO: Discuss private/public on this attribute

        public WidgetAttribute(string name, int icon)
        {
            if (name == null) throw new ArgumentNullException();
            Name = name;
            Icon = icon;
        }
    }
}
