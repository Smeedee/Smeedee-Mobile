using System;
using Android.Graphics;
using Java.Lang;

namespace Smeedee.Android
{
    public class ColorTools
    {
        public static Color GetColorFromHex(string hex)
        {
            if (hex.Substring(0, 1) == "#") hex = hex.Substring(1);
            if (hex.Length == 3) hex = hex + hex;
            if (hex.Length != 6) throw new ArgumentException();
            var r = Integer.ParseInt(hex.Substring(0, 2), 16);
            var g = Integer.ParseInt(hex.Substring(2, 2), 16);
            var b = Integer.ParseInt(hex.Substring(4, 2), 16);
            return new Color(r, g, b);
        }
    }
}
