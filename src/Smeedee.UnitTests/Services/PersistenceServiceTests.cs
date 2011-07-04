using System;
using System.Collections.Generic;
using NUnit.Framework;
using Smeedee.Services;
using Smeedee.UnitTests.Resources;


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
            Assert.AreEqual("\"Hello World\"", _fakeKvStorage.savedValues[KEY]);
        }

        [Test]
        public void should_save_simple_list_with_ints()
        {
            _persistenceService.Save(KEY, new List<int>() {1, 2, 3, 4});
            Assert.AreEqual("[1,2,3,4]", _fakeKvStorage.savedValues[KEY]);
        }

        [Test]
        public void should_save_simple_custom_data_structure()
        {
            _persistenceService.Save(KEY, new SimpleDataStructure()
                                              {
                                                  var1 = 10.2, 
                                                  var2 = true, 
                                                  var3 = "Test"
                                              });
            Assert.AreEqual(PersistenceServiceResources.SimpleCustomClassJson, 
                            _fakeKvStorage.savedValues[KEY]);
        }
    }

    internal class SimpleDataStructure
    {
        public double var1;
        public bool var2;
        public string var3;
    }
}
