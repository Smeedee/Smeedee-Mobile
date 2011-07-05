using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Smeedee.Services;
using Smeedee.Utilities;

namespace Smeedee.IntegrationTests
{
    [TestFixture]
    public class ImageServiceTests
    {
        [Test]
        public void Should_respond_with_a_byte_array_when_GetImage_is_called()
        {
            var imageService = new ImageService(new NoBackgroundWorker());
            byte[] recievedBytes = null;
            imageService.GetImage(new Uri("http://www.dagbladet.no/favicon.ico"),
                                  bytes => recievedBytes = bytes);
            Assert.IsNotNull(recievedBytes);
            Assert.AreNotEqual(0, recievedBytes.Length);
        }
    }
}
