﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee
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
            throw new NotImplementedException();
            //TODO: Get filename from persistance, and use regular disk access to get the image
            /*
            var cacheKey = "image_cache." + uri;
            var cached = cache.Get<byte[]>(cacheKey, null);
            if (cached == null)
            {
                serviceToCache.GetImage(uri, (bytes) => CacheAndCallback(bytes, cacheKey, callback));
            } else
            {
                callback(cached);
            }
             */
        }

        private void CacheAndCallback(byte[] bytes, string cacheKey, Action<byte[]> callback)
        {
            throw new NotImplementedException(); 
            //TODO: Save filenames to persistance, and use regular disk access to save the images
            /*
            cache.Save(cacheKey, bytes);
            callback(bytes);*/
        }
    }
}
