using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;

using System.Windows.Media.Imaging;
using Smeedee.Model;

namespace Smeedee.WP7.Converters
{
    public class UriToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
                                object parameter, CultureInfo culture)
        {
            var image = new BitmapImage(new Uri("pack://application/Resources/Images/default_person.jpeg"));
            var imageService = SmeedeeApp.Instance.ServiceLocator.Get<IImageService>();
            imageService.GetImage(value as Uri, bytes =>
            {
                if (bytes == null) return;
                Deployment.Current.Dispatcher.BeginInvoke(
                    () => image.SetSource(new MemoryStream(bytes)));
            });
            return image;
        }

        public object ConvertBack(object value, Type targetType,
                                    object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
