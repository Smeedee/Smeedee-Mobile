using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Smeedee.Utilities;

namespace Smeedee.Services
{
    public class ImageService : IImageService
    {
        private IBackgroundWorker worker;

        public ImageService(IBackgroundWorker worker)
        {
            this.worker = worker;
        }

        public void GetImage(Uri uri, Action<byte[]> callback)
        {
            //Usage Android:
            //var img = new ImageView();
            //var bmp = new BitmapFactory().decodeByteArray(bytes);
            //img.setImageBitmap(bmp);
            
            //Usage iPhone:
            //UIImage img = UIImage.LoadFromData(NSData.FromArray(bytes));
            
            worker.Invoke(() =>{
                var webClient = new WebClient();
                var bytes = webClient.DownloadData(uri);
                callback(bytes);
            });
        }
    }
}
