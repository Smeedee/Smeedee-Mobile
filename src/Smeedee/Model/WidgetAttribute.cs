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
        public bool IsEnabled { get; set; } //TODO: Discuss private/public on this attribute

        public WidgetAttribute(string name, string icon)
        {
            if (name == null || icon == null) throw new ArgumentNullException();
            Name = name;
            Icon = icon;
        }
    }
}
