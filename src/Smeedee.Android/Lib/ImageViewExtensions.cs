using System;
using Android.App;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android.Lib
{
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
                                               ((Activity) self.Context).RunOnUiThread(() => self.SetImageBitmap(bmp));
                                           });
        }
    }
}