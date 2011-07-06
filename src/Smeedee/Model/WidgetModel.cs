using System;

namespace Smeedee.Model
{
    public class WidgetModel
    {
        public string Name { get; private set; }
        public string Icon { get; private set; }
        public Type Type { get; private set; }
        public bool IsEnabled { get; private set; }
        public WidgetModel(string name, string icon, Type type, bool isEnabled)
        {
            if (name == null || icon == null || type == null) throw new ArgumentNullException();
            Name = name;
            Icon = icon;
            Type = type;
            IsEnabled = isEnabled;
        }
    }
}