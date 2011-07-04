using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using Smeedee.Properties;

namespace Smeedee.Services
{
    public class FakeImageService :IImageService
    {
        private byte[] bytes;
        private IBackgroundWorker worker;
        private Image defaultImage = Resources.default_person;
        

        public FakeImageService(IBackgroundWorker worker)
        {
            this.worker = worker;
            this.bytes = imageToByteArray(defaultImage);
        }

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
            worker.Invoke(() => callback(bytes));
        }

        public byte[] imageToByteArray(Image imageIn)
        {
            MemoryStream ms = new MemoryStream();
            imageIn.Save(ms, ImageFormat.Jpeg);
            return ms.ToArray();
        }
    }
}
