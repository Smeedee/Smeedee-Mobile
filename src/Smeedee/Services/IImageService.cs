using System;

namespace Smeedee.Services
{
    public interface IImageService
    {
        void GetImage(Uri uri, Action<byte[]> callback);
    }
}