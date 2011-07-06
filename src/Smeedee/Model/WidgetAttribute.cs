using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Model
{
    [AttributeUsage(AttributeTargets.Class)]
    public class WidgetAttribute : Attribute
    {
        public string Name { get; set; }
        public string Icon { get; set; }

        public WidgetAttribute(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }
    }
}
