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
using Smeedee.Services;

namespace Smeedee.WP7.Lib
{
    public class NoLog : ILog
    {
        public void Log(string message, params string[] more)
        {
        }
    }
}
