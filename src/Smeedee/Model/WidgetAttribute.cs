using System;
using Smeedee;
using Smeedee.Lib;

namespace Smeedee.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetAttribute : Attribute
    {
        public string Name { get; private set; }
        public string StaticDescription { get; set; }
        public Type SettingsType { get; set; }

        public WidgetAttribute(string name)
        {
            Guard.NotNull(name);
            Name = name;
        }
    }
}
