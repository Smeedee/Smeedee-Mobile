using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Services;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class CachedImageServiceTests
    {
        private FakeImageService imageService;
        private FakePersistenceService cache;
        private CachedImageService cachedImageService;

        [SetUp]
        public void SetUp()
        {
            imageService = new FakeImageService(new NoBackgroundWorker());
            cache = new FakePersistenceService();
            cachedImageService = new CachedImageService(imageService, cache);
        }

        [Test]
        public void Should_actually_use_cache()
        {
            cachedImageService.GetImage(new Uri("http://example.com"), (bytes) => {});

            Assert.AreEqual(1, cache.GetCalls);
            Assert.AreEqual(1, cache.SaveCalls);
        }

        [Test]
        public void Should_hit_underlying_image_service_only_once_for_same_uri()
        {
            var imageService = new MyFakeImageService();
            var cachedImageService = new CachedImageService(imageService, cache);
            for (int i = 0; i < 5; ++i)
                cachedImageService.GetImage(new Uri("http://example.com"), (bytes) => { });

            Assert.AreEqual(1, imageService.GetCalls);
        }

        [Test]
        public void Should_call_the_given_callback_when_loaded()
        {
            var callbacks = 0;
            cachedImageService.GetImage(new Uri("http://example.com"), (bytes) => callbacks += 1);
            cachedImageService.GetImage(new Uri("http://example.com"), (bytes) => callbacks += 1);

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

    internal class FakePersistenceService : IPersistenceService
    {
        public int SaveCalls, GetCalls;
        private Dictionary<string, object> cache = new Dictionary<string, object>();
        public void Save(string key, object value)
        {
            SaveCalls += 1;
            cache[key] = value;
        }

        public T Get<T>(string key, T defaultObject)
        {
            GetCalls += 1;
            return cache.ContainsKey(key) ? (T)cache[key] : defaultObject;
        }
    }
}
