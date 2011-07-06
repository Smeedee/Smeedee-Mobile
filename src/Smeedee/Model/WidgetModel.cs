using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Model
{
    public class WidgetModel
    {
        public string Name { get; set; }
        public string Icon { get; set; }
        public Type Type { get; set; }
        public bool IsEnabled { get; set; }
    }
}