using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class DiskCachedImageServiceTests
    {
        private FakeImageService imageService;
        private FakePersistenceService cache;
        private DiskCachedImageService _diskCachedImageService;
        private SmeedeeApp app = SmeedeeApp.Instance;

        [SetUp]
        public void SetUp()
        {
            app.ServiceLocator.Bind(new Directories {CacheDir = "C:\\"});
            imageService = new FakeImageService(new NoBackgroundInvocation());
            cache = new FakePersistenceService();
            _diskCachedImageService = new DiskCachedImageService(imageService);
        }

        [Test]
        public void Should_actually_use_cache()
        {
            var uri = new Uri("http://example.com");
            _diskCachedImageService.GetImage(uri, (bytes) => {});
            File.Delete("C:\\Smeedee_img" + uri.GetHashCode());
            Assert.AreEqual(1, imageService.GetImageCalls);
        }

        [Test]
        public void Should_hit_underlying_image_service_only_once_for_same_uri()
        {
            var imageService = new MyFakeImageService();
            var cachedImageService = new DiskCachedImageService(imageService);
            var uri = new Uri("http://example.com");
            for (int i = 0; i < 5; ++i)
                cachedImageService.GetImage(uri, (bytes) => { });
            File.Delete("C:\\Smeedee_img" + uri.GetHashCode());

            Assert.AreEqual(1, imageService.GetCalls);
        }

        [Test]
        public void Should_call_the_given_callback_when_loaded()
        {
            var callbacks = 0;
            var uri = new Uri("http://example.com");
            _diskCachedImageService.GetImage(uri, (bytes) => callbacks += 1);
            _diskCachedImageService.GetImage(uri, (bytes) => callbacks += 1);
            File.Delete("C:\\Smeedee_img" + uri.GetHashCode());

            Assert.AreEqual(2, callbacks);
        }
    }

    internal class MyFakeImageService : IImageService
    {
        public int GetCalls;
        public void GetImage(Uri uri, Action<byte[]> callback)
        {
            callback(new byte[]{1,2,3});
            GetCalls += 1;
        }
    }
}
