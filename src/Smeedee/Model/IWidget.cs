using System;

namespace Smeedee.Model
{
    public interface IWidget
    {
        void Refresh();
        string GetDynamicDescription();
        event EventHandler DescriptionChanged;
    }
}
