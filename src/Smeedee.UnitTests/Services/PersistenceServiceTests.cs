using System;
using System.Collections.Generic;
using NUnit.Framework;
using Smeedee.Services;
using Smeedee.UnitTests.Resources;


namespace Smeedee.UnitTests.Services
{
    internal abstract class PersistenceServiceTests
    {
        protected bool LooksLikeBase64(string str)
        {
            var looksValid = true;
            try
            {
                byte[] bytes = Convert.FromBase64String(str);
            }
            catch
            {
                looksValid = false;
            }
            return looksValid;
        }
        protected FakeKVStorage _fakeKvStorage;
        protected PersistenceService _persistenceService;
        protected const string KEY = "DUMMY_KEY";

        [TestFixture]
        public class When_saving : PersistenceServiceTests
        {
            [SetUp]
            public void SetUp()
            {
                _fakeKvStorage = new FakeKVStorage();
                _persistenceService = new PersistenceService(_fakeKvStorage);
            }

            [Test]
            public void should_ask_to_save_simple_int_argument_as_base64_string()
            {
                _persistenceService.Save(KEY, 1);
                Assert.True(LooksLikeBase64(_fakeKvStorage.savedValues[KEY]));
            }

            [Test]
            public void should_ask_to_save_simple_string_argument_as_base64_string()
            {
                _persistenceService.Save(KEY, "Hello World");
                Assert.True(LooksLikeBase64(_fakeKvStorage.savedValues[KEY]));
            }

            [Test]
            public void should_ask_to_save_simple_list_with_ints_as_base64_string()
            {
                _persistenceService.Save(KEY, new List<int>() {1, 2, 3, 4});
                Assert.True(LooksLikeBase64(_fakeKvStorage.savedValues[KEY]));
            }

            [Test]
            public void should_ask_to_save_simple_custom_data_structure_as_base64_string()
            {
                _persistenceService.Save(KEY, new SerializableSimpleDataStructure()
                                                  {
                                                      var1 = 10.2,
                                                      var2 = true,
                                                      var3 = "Test"
                                                  });
                Assert.True(LooksLikeBase64(_fakeKvStorage.savedValues[KEY]));
            }
        }

        [TestFixture]
        public class When_retrieving : PersistenceServiceTests
        {
            [SetUp]
            public void SetUp()
            {
                _fakeKvStorage = new FakeKVStorage();
                _persistenceService = new PersistenceService(_fakeKvStorage);
            }

            [Test]
            public void Should_properly_deserialize_simple_int_describes_as_json()
            {
                _fakeKvStorage.retrievableContent = "1";
                Assert.AreEqual(1, _persistenceService.Get<int>(KEY, 42));
            }

            [Test]
            public void Should_properly_deserialize_simple_string_describes_as_json()
            {
                _fakeKvStorage.retrievableContent = "\"Hello World\"";
                Assert.AreEqual("Hello World", _persistenceService.Get<string>(KEY, "This is wrong"));
            }

            [Test]
            public void Should_properly_deserialize_simple_list_of_ints_described_as_json()
            {
                _fakeKvStorage.retrievableContent = "[1,2,3,4]";
                Assert.AreEqual(new List<int>() {1, 2, 3, 4}, _persistenceService.Get(KEY, new List<int>()));
            }

            [Test]
            public void Should_properly_deserialize_simple_custom_object_describes_as_json()
            {
                _fakeKvStorage.retrievableContent = PersistenceServiceResources.SimpleCustomClassJson;
                Assert.AreEqual(new SerializableSimpleDataStructure()
                                    {
                                        var1 = 10.2,
                                        var2 = true,
                                        var3 = "Test"
                                    }, _persistenceService.Get<SerializableSimpleDataStructure>(KEY, null));
            }
        }

        [TestFixture]
        public class When_retrieving_should_return_default_object : PersistenceServiceTests
        {
            [SetUp]
            public void SetUp()
            {
                _fakeKvStorage = new FakeKVStorage();
                _persistenceService = new PersistenceService(_fakeKvStorage);
            }

            [Test]
            public void Should_return_default_if_key_does_not_exist()
            {
                Assert.AreEqual(1, _persistenceService.Get<int>("Non existing key", 1));
            }

            [Test]
            public void Should_return_default_if_returned_value_can_not_be_deserialzed_to_given_type()
            {
                _persistenceService.Save(KEY, "Hello World");
                Assert.AreEqual(1, _persistenceService.Get<int>(KEY, 1));
            }
        }
    }


#pragma warning disable
    internal class UnserializableSimpleDataStructure
    {
        public double var1;
        public bool var2;
        public string var3;

        public override bool Equals(object obj)
        {
            var other = (SerializableSimpleDataStructure) obj;
            return (var1 == other.var1 && var2 == other.var2 && var3 == other.var3);
        }
    }

    [Serializable]
    internal class SerializableSimpleDataStructure
    {
        public double var1;
        public bool var2;
        public string var3;

        public override bool Equals(object obj)
        {
            var other = (SerializableSimpleDataStructure) obj;
            return (var1 == other.var1 && var2 == other.var2 && var3 == other.var3);
        }
    }
#pragma warning restore
}
