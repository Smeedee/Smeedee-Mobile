using System;

namespace Smeedee.Model
{
    public interface IWidget
    {
        void Refresh();
        DateTime LastRefreshTime();
        string GetDynamicDescription();
        event EventHandler DescriptionChanged;
    }
}
