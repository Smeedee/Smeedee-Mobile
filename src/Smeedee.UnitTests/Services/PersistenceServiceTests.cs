using System.Collections.Generic;
using NUnit.Framework;
using Smeedee.Services;


namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    class PersistenceServiceTests
    {
        protected FakeKVStorage _fakeKvStorage;
        protected PersistenceService _persistenceService;
        protected const string KEY = "TEST_KEY";

        [SetUp]
        public void SetUp()
        {
            _fakeKvStorage = new FakeKVStorage();
            _persistenceService = new PersistenceService(_fakeKvStorage);
        }

        [Test]
        public void should_save_simple_int_argument()
        {
            _persistenceService.Save(KEY, 1);
            Assert.AreEqual("1", _fakeKvStorage.savedValues[KEY]);
        }

        [Test]
        public void should_save_simple_string_argument()
        {
            _persistenceService.Save(KEY, "Hello World");
            Assert.AreEqual("Hello World", _fakeKvStorage.savedValues[KEY]);
        }

        
    }
}
