using Smeedee.Services;
using Logger = Android.Util;

namespace Smeedee.Android.Services
{
    public class LogService : ILog
    {
        public void Log(string message, params string[] more)
        {
            Logger.Log.Info(message, string.Join(" ", more));
        }
    }
}