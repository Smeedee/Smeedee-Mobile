using System;
using Android.Graphics;

namespace Smeedee.Android
{
    public static class ColorTools
    {
        public static Color GetColorFromHex(string hex)
        {
            if (hex.Substring(0, 1) == "#") hex = hex.Substring(1);
            if (hex.Length == 3) hex = hex + hex;
            if (hex.Length != 6) throw new ArgumentException();
            var r = Convert.ToInt32(hex.Substring(0, 2), 16);
            var g = Convert.ToInt32(hex.Substring(2, 2), 16);
            var b = Convert.ToInt32(hex.Substring(4, 2), 16);
            return new Color(r, g, b);
        }
    }
}