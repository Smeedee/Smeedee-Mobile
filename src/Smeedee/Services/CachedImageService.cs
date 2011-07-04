using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Services
{
    public class CachedImageService : IImageService
    {
        public CachedImageService(IImageService serviceToCache)
        {
            
        }

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
            //if (Cached)
        }
    }
}
