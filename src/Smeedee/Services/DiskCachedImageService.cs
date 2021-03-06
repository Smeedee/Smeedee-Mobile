using System;
using System.IO;
using System.Linq;
using Smeedee.Model;
using Smeedee.Lib;

namespace Smeedee.Services
{
    public class DiskCachedImageService : IImageService
    {
        private IImageService serviceToCache;
        private string cachePath;
        private IFileIO fileReader;
        public const string DEFAULT_URI = "smeedee://default_person.png";

        public DiskCachedImageService(IImageService serviceToCache)
        {
            this.serviceToCache = serviceToCache;
            this.cachePath = SmeedeeApp.Instance.ServiceLocator.Get<Directories>().CacheDir;
            this.fileReader = SmeedeeApp.Instance.ServiceLocator.Get<IFileIO>();
        }

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
            var fileName = cachePath + "Smeedee_img" + uri.GetHashCode();

            if (File.Exists(fileName))
            {
                var bytes = fileReader.ReadAllBytes(fileName);
                if (IsMissingImagePlaceholder(bytes))
                    bytes = null;
                callback(bytes);
            } else
            {
                serviceToCache.GetImage(uri, bytes => SaveAndCallback(bytes, fileName, callback));
            }
        }

        private void SaveAndCallback(byte[] bytes, string fileName, Action<byte[]> callback)
        {
            bytes = bytes ?? GetMissingImagePlaceholder();
            lock (this)
            {
                fileReader.WriteAllBytes(fileName, bytes);
            }
            callback(bytes);
        }

        private byte[] GetMissingImagePlaceholder()
        {
            return new byte[] {0, 0, 0};
        }

        private bool IsMissingImagePlaceholder(byte[] bytes)
        {
            return bytes.Length == 3 && bytes.All(b => b == 0);
        }
    }
}