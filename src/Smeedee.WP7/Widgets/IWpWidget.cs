using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using Smeedee.Model;

namespace Smeedee.WP7.Widgets
{
    public interface IWpWidget : IWidget
    {
        PivotItem View { get; set; }
    }
}
