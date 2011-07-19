using System;
using System.Drawing;
using System.IO;
using NUnit.Framework;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class FakeImageServiceTests
    {
        [Test]
        public void Service_should_return_non_empty_byte_array_for_any_uri()
        {
            var imgService = new FakeImageService(new NoBackgroundInvocation());
            imgService.GetImage(new Uri("http://www.example.com"), (bytes) =>
                {
                    Assert.IsNotNull(bytes);
                    Assert.IsNotEmpty(bytes);
                });
        }

        [Test]
        public void Service_should_return_byte_array_that_can_be_converted_to_image()
        {
            var imgService = new FakeImageService(new NoBackgroundInvocation());
            imgService.GetImage(new Uri("http://www.example.com"), (bytes) =>
            {
                var img = Image.FromStream(new MemoryStream(bytes));
                Assert.AreNotEqual(img.Height, 0);
            });
        }
    }
}
