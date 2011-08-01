using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android
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

    public static class ImageViewExtensions
    {
        public static void LoadUriOrDefault(this ImageView self, Uri uri, int defaultImage)
        {
            self.SetImageResource(defaultImage);

            var imageService = SmeedeeApp.Instance.ServiceLocator.Get<IImageService>();
            imageService.GetImage(uri, bytes =>
            {
                if (bytes == null)
                    return;
                var bmp = BitmapCache.BitmapFromBytes(bytes);
                ((Activity)self.Context).RunOnUiThread(() => self.SetImageBitmap(bmp));
            });
        }
    }
}