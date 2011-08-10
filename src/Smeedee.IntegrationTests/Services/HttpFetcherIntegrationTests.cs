using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Services.Fakes;

namespace Smeedee.IntegrationTests.Services
{
    class HttpFetcherIntegrationTests
    {
        [SetUp]
        public void SetUp()
        {
            SmeedeeApp.Instance.ServiceLocator.Bind<ILog>(new FakeLogService());
        }

        [Test]
        public void Should_gracefully_return_empty_string_when_encountering_an_error_code_such_as_404()
        {
            
            var fetcher = new HttpFetcher();
            Assert.AreEqual("", fetcher.DownloadString("http://services.smeedee.org/This_url_does_not_exist"));
        }
    }
}
