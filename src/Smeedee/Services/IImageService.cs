using System;

namespace Smeedee
{
    public interface IImageService
    {
        void GetImage(Uri uri, Action<byte[]> callback);
    }
}