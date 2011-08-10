using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.IntegrationTests
{
    [TestFixture]
    public class ImageServiceTests
    {
        private byte[] GetImageBytesFromDagbladet()
        {
            SmeedeeApp.Instance.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            var imageService = new ImageService();
            byte[] recievedBytes = null;
            imageService.GetImage(new Uri("http://www.dagbladet.no/favicon.ico"),
                                  bytes => recievedBytes = bytes);
            return recievedBytes;
        }

        [Test]
        [Ignore]
        public void Should_respond_with_a_byte_array_when_GetImage_is_called()
        {
            byte[] bytes = GetImageBytesFromDagbladet();
            Assert.IsNotNull(bytes);
            Assert.AreNotEqual(0, bytes.Length);
        }

        [Test]
        [Ignore]
        public void Recieved_byte_array_should_be_parseable_as_an_image()
        {
            byte[] bytes = GetImageBytesFromDagbladet();
            var img = Image.FromStream(new MemoryStream(bytes));
            Assert.AreNotEqual(0, img.Height);
        }
    }
}
