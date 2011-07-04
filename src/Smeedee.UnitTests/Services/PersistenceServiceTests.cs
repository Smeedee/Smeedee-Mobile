using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Script.Serialization;
using NUnit.Framework;
using Smeedee.Services;


namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    class PersistenceServiceTests
    {
        private FakeKVStorage _fakeKvStorage;
        private PersistenceService _persistenceService;
        private const string KEY = "TEST_KEY";

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
    }

    internal class MyClass
    {
        public int LOL { get; set; }
        public string lol2 { get; set; }
    }
}
