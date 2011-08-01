using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public class MemoryCachedImageService : IImageService
    {
        private IImageService serviceToCache;
        private Dictionary<Uri, bool> isLoading; 
        private Dictionary<Uri, byte[]> images; 
        private Dictionary<Uri, List<Action<byte[]>>> callbacks;

        public MemoryCachedImageService(IImageService serviceToCache)
        {
            this.serviceToCache = serviceToCache;
            isLoading = new Dictionary<Uri, bool>();
            images = new Dictionary<Uri, byte[]>();
            callbacks = new Dictionary<Uri, List<Action<byte[]>>>();
        }

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
            lock (this)
            {
                if (!isLoading.ContainsKey(uri))
                {
                    isLoading[uri] = true;
                    StoreCallback(uri, callback);
                    serviceToCache.GetImage(uri, ImageRetriever(uri));
                } else if (isLoading[uri])
                {
                    StoreCallback(uri, callback);
                } else
                {
                    callback(images[uri]);
                }
            }
        }

        private Action<byte[]> ImageRetriever(Uri uri)
        {
            return (bytes) =>
                       {
                           isLoading[uri] = false;
                           images[uri] = bytes;
                           FireCallbacks(uri);
                       };
        }

        private void StoreCallback(Uri uri, Action<byte[]> callback)
        {
            if (callbacks.ContainsKey(uri))
            {
                callbacks[uri].Add(callback);
            } else
            {
                callbacks[uri] = new List<Action<byte[]>> {callback};
            }
        }

        private void FireCallbacks(Uri uri)
        {
            foreach (var callback in callbacks[uri])
                callback(images[uri]);
        }
    }
}
