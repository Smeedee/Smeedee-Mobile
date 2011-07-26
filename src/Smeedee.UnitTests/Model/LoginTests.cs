using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class LoginTests
    {
        private Login login;
        private FakePersistenceService fakePersistenceService;

        [SetUp]
        public void SetUp()
        {
            fakePersistenceService = new FakePersistenceService();
            SmeedeeApp.Instance.ServiceLocator.Bind<IPersistenceService>(fakePersistenceService);
            login = new Login();
        }

        [Test]
        public void Should_have_non_null_default_key()
        {
            Assert.NotNull(login.Key);
        }

        [Test]
        public void Should_have_non_null_default_url()
        {
            Assert.NotNull(login.Url);
        }

        [Test]
        public void Get_and_set_should_correspond_for_key()
        {
            login.Key = "newkey";
            Assert.AreEqual("newkey", login.Key);
        }

        [Test]
        public void Get_and_set_should_correspond_for_url()
        {
            login.Url = "newkey";
            Assert.AreEqual("newkey", login.Url);
        }

        [Test]
        public void Setters_should_hit_persistence()
        {
            login.Key = "newkey";
            login.Url = "newkey";
            Assert.AreEqual(2, fakePersistenceService.SaveCalls);
        }

        [Test]
        public void Getters_should_hit_persistence()
        {
            var key = login.Key;
            var url = login.Url;
            Assert.AreEqual(2, fakePersistenceService.GetCalls);
        }
		
		[Test]
		public void Should_store_url_when_changing_server()
		{
			login.StoreAndValidate("new url", "", (str) => {});
			
			Assert.AreEqual("new url", login.Url);
		}
		
    }
}
