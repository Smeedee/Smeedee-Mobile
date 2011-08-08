using Microsoft.Phone.Controls;
using Smeedee.Model;

namespace Smeedee.WP7.Widgets
{
    public interface IWpWidget : IWidget
    {
        PivotItem View { get; set; }
    }
}
