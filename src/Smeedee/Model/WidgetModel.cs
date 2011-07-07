using System;

namespace Smeedee.Model
{
    public class WidgetModel
    {
        public string Name { get; private set; }
        public int Icon { get; private set; }
        public Type Type { get; private set; }
        public bool IsEnabled { get; set; }

        public WidgetModel(string name, int icon, Type type, bool isEnabled)
        {
            if (name == null || type == null) throw new ArgumentNullException();
            Name = name;
            Icon = icon;
            Type = type;
            IsEnabled = isEnabled;
        }
    }
}