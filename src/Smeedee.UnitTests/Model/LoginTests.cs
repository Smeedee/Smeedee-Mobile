using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
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
			SmeedeeApp.Instance.ServiceLocator.Bind<IValidationService>(new FakeValidationService());
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
        public void Should_keep_urls()
        {
            login.Url = "http://www.example.com/";
            Assert.AreEqual("http://www.example.com/", login.Url);
        }

        [Test]
        public void Should_add_a_slash_to_the_end_of_urls()
        {
            login.Url = "http://www.example.com";
            Assert.AreEqual("http://www.example.com/", login.Url);
        }

        [Test]
        public void Should_add_http_to_the_beginning_of_urls_without_specified_protocol()
        {
            login.Url = "www.example.com";
            Assert.AreEqual("http://www.example.com/", login.Url);
        }

        [Test]
        public void Should_not_alter_specficied_protocols()
        {
            login.Url = "https://www.example.com";
            Assert.AreEqual("https://www.example.com/", login.Url);
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
			login.StoreAndValidate("https://www.example.com/", "", (str) => {});
			
			Assert.AreEqual("https://www.example.com/", login.Url);
		}
		[Test]
		public void Should_store_key_when_changing_server()
		{
			login.StoreAndValidate("", "key", (str) => {});
			
			Assert.AreEqual("key", login.Key);
		}
		
		[Test]
		public void Callback_should_be_run_when_changing_server() 
		{
			var shouldBeTrue = false;
			login.StoreAndValidate("", "", (str) => shouldBeTrue = true);
			Assert.IsTrue(shouldBeTrue);
		}
		
		[Test]
		public void Should_return_sucess_when_correct_validation_against_server()
		{
			string s = "";
			login.StoreAndValidate("http://www.example.com/", "1234", (str) => s = str);
			
			Assert.AreEqual(Login.ValidationSuccess, s);
		}
		[Test]
		public void Should_return_failed_when_wrong_validation_against_server()
		{
			string s = "";
			login.StoreAndValidate("http://www.example.com/", "failkey", (str) => s = str);
			
			Assert.AreEqual(Login.ValidationFailed, s);
		}
		
    }
	
	public class FakeValidationService : IValidationService
	{
		public void Validate(string url, string key, Action<bool> callback)
		{
			callback(url == "http://www.example.com/" && key == "1234");
		}
	}
}
