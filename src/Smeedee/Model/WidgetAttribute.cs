using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetAttribute : Attribute
    {
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public bool IsEnabled { get; private set; }
        public Type Type { get; private set; }

        public WidgetAttribute(string name, string icon)
        {
            if (name == null || icon == null) throw new ArgumentNullException();
            Name = name;
            Icon = icon;
        }
    }
}
