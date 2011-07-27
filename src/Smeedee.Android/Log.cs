using Android.Util;

namespace Smeedee.Android
{
    public class AndroidLog : ILog
    {
        public void Debug(string str)
        {
            Log.Debug("Smeedee", str);
        }
    }
}