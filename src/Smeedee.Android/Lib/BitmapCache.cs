using System.Collections.Generic;
using Android.Graphics;

namespace Smeedee.Android.Lib
{
    public static class BitmapCache
    {
        private static Dictionary<byte[], Bitmap> cache = new Dictionary<byte[], Bitmap>(); 
        public static Bitmap BitmapFromBytes(byte[] bytes)
        {
            if (!cache.ContainsKey(bytes))
            {
                var bmp = BitmapFactory.DecodeByteArray(bytes, 0, bytes.Length);
                cache[bytes] = bmp;
            }
            return cache[bytes];
        }
    }
}