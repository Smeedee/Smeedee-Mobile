<<<<<<< HEAD
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class MemoryCachedImageServiceTests
    {
        private FakeImageService fakeImageService;
        private MemoryCachedImageService service;

        [SetUp]
        public void SetUp()
        {
            fakeImageService = new FakeImageService(new NoBackgroundInvocation());
            service = new MemoryCachedImageService(fakeImageService);
        }

        [Test]
        public void Should_hit_the_underlying_image_service()
        {
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { });
            Assert.AreEqual(1, fakeImageService.GetImageCalls);
        }

        [Test]
        public void Should_hit_the_underlying_image_service_only_once_per_uri()
        {
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { });
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { });
            Assert.AreEqual(1, fakeImageService.GetImageCalls);
        }

        [Test]
        public void Should_call_callback_on_load()
        {
            var callbacks = 0;
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { callbacks += 1; });
            Assert.AreEqual(1, callbacks);
        }

        [Test]
        public void Should_call_all_callbacks_on_load()
        {
            var callbacks = 0;
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { callbacks += 1; });
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { callbacks += 1; });
            Assert.AreEqual(2, callbacks);
        }
    }
}
=======
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class MemoryCachedImageServiceTests
    {
        private FakeImageService fakeImageService;
        private MemoryCachedImageService service;

        [SetUp]
        public void SetUp()
        {
            fakeImageService = new FakeImageService(new NoBackgroundInvocation());
            service = new MemoryCachedImageService(fakeImageService);
        }

        [Test]
        public void Should_hit_the_underlying_image_service()
        {
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { });
            Assert.AreEqual(1, fakeImageService.GetImageCalls);
        }

        [Test]
        public void Should_hit_the_underlying_image_service_only_once_per_uri()
        {
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { });
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { });
            Assert.AreEqual(1, fakeImageService.GetImageCalls);
        }

        [Test]
        public void Should_call_callback_on_load()
        {
            var callbacks = 0;
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { callbacks += 1; });
            Assert.AreEqual(1, callbacks);
        }

        [Test]
        public void Should_call_all_callbacks_on_load()
        {
            var callbacks = 0;
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { callbacks += 1; });
            service.GetImage(new Uri("http://www.example.com"), (bytes) => { callbacks += 1; });
            Assert.AreEqual(2, callbacks);
        }
    }
}
>>>>>>> d533814753eb544fda6db1754fcdfa845dfd6594
