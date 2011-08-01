<<<<<<< HEAD
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android
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
                ((Activity)self.Context).RunOnUiThread(() => self.SetImageBitmap(bmp));
            });
        }
    }
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Smeedee.Model;

namespace Smeedee.Android
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
                ((Activity)self.Context).RunOnUiThread(() => self.SetImageBitmap(bmp));
            });
        }
    }
>>>>>>> d533814753eb544fda6db1754fcdfa845dfd6594
}