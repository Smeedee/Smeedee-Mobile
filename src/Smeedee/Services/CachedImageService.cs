using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Services
{
    public class CachedImageService : IImageService
    {
        private IPersistenceService cache;
        private IImageService serviceToCache;

        public CachedImageService(IImageService serviceToCache, IPersistenceService cache)
        {
            this.serviceToCache = serviceToCache;
            this.cache = cache;
        }

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
            var cacheKey = "image_cache." + uri.ToString();
            var cached = cache.Get<byte[]>(cacheKey, null);
            if (cached == null)
            {
                serviceToCache.GetImage(uri, (bytes) => CacheAndCallback(bytes, cacheKey, callback));
            } else
            {
                callback(cached);
            }
        }

        private void CacheAndCallback(byte[] bytes, string cacheKey, Action<byte[]> callback)
        {
            cache.Save(cacheKey, bytes);
            callback(bytes);
        }
    }
}
