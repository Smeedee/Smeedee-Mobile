using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class ValidationServiceTests
    {
        private FakeHttpFetcher fakeHttpFetcher = new FakeHttpFetcher("");
        private SmeedeeApp app = SmeedeeApp.Instance;
        private ValidationService validator;

        [SetUp]
        public void SetUp()
        {
            app.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            app.ServiceLocator.Bind<IFetchHttp>(fakeHttpFetcher);
            validator = new ValidationService();
        }

        [Test]
        public void Should_return_false_when_web_call_returns_false()
        {
            fakeHttpFetcher.SetHtmlString("False");
            var wasValid = true;
            validator.Validate("http://example.com", "", valid => wasValid = valid);
            Assert.False(wasValid);
        }

        [TestCase("")]
        [TestCase("notTrue")]
        [TestCase("failed")]
        public void Should_return_false_when_web_call_returns_anything_other_than_true(string key)
        {
            fakeHttpFetcher.SetHtmlString("");
            var wasValid = true;
            validator.Validate("http://example.com", key, valid => wasValid = valid);
            Assert.False(wasValid);
        }

        [Test]
        public void Should_return_true_when_web_call_returns_true()
        {
            fakeHttpFetcher.SetHtmlString("True");
            var wasValid = false;
            validator.Validate("http://example.com", "", valid => wasValid = valid);
            Assert.True(wasValid);
        }

        [Test]
        public void Should_make_call_to_the_given_url()
        {
            var url = "http://example.com";
            validator.Validate(url, "key123", valid => { });
            var expectedUrl = url + ServiceConstants.MOBILE_SERVICES_RELATIVE_PATH +
                              ServiceConstants.VALIDATION_SERVICE_URL + "?apiKey=key123";
            Assert.AreEqual(expectedUrl, fakeHttpFetcher.UrlAskedFor);
        }
    }
}
