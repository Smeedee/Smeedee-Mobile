using System;

namespace Smeedee.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetAttribute : Attribute
    {
        public string Name { get; private set; }
        public string StaticDescription { get; set; }

        public WidgetAttribute(string name)
        {
            Guard.NotNull(name);
            Name = name;
        }
    }
}
