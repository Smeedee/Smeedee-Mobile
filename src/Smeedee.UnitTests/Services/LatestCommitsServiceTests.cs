using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Services
{
    public class LatestCommitsServiceTests
    {
        protected FakeHttpFetcher downloader;
        protected ILatestCommitsService service;

        [SetUp]
        public void SetUp()
        {
            downloader = new FakeHttpFetcher("foo");
            SmeedeeApp.Instance.ServiceLocator.Bind<IFetchHttp>(downloader);
            SmeedeeApp.Instance.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            SmeedeeApp.Instance.ServiceLocator.Bind<IPersistenceService>(new FakePersistenceService());
            service = new LatestCommitsService();
        }

        [Test]
        public void Should_correctly_parse_build_name()
        {
            downloader.SetHtmlString("TestMessage\f20110722112921\fTestUser");
            service.Get10(1, r => Assert.AreEqual("TestMessage", r.First().Message));
        }

        [Test]
        public void Should_correctly_parse_user_name()
        {
            downloader.SetHtmlString("TestMessage\f20110722112921\fTestUser");
            service.Get10(1, r => Assert.AreEqual("TestUser", r.First().User));
        }

        [Test]
        public void Should_correctly_parse_date()
        {
            downloader.SetHtmlString("TestMessage\f20110722112921\fTestUser");
            service.Get10(1, r => Assert.AreEqual(DateTime.ParseExact("20110722112921", "yyyyMMddHHmmss", CultureInfo.InvariantCulture), r.First().Date));
        }

        [Test]
        public void Should_only_return_one_line_if_there_is_no_line_delimiter()
        {
            downloader.SetHtmlString("TestMessage\f20110722112921\fTestUser");
            service.Get10(1, r => Assert.AreEqual(1, r.Count()));
        }

        [Test]
        public void Should_identify_and_parse_commits_following_a_newline_delimiter()
        {
            downloader.SetHtmlString("TestMessage\f20110722112921\fTestUser\aTestMessage2\f20110722112921\fTestUser2");
            service.Get10(1, r => Assert.AreEqual("TestMessage2", r.Skip(1).First().Message));
        }

        [Test]
        public void Should_gracefully_handle_malformed_http_results_by_returning_only_the_well_formed_lines_of_the_result()
        {
            downloader.SetHtmlString("TestMessage\f20110722112921\fTestUser\aBrokenTestMessage\f2011072asd2112921\fTestUser\aTestMessage2\f20110722112921\fTestUser2");
            service.Get10(1, r => Assert.AreEqual(2, r.Count()));
        }

        [Test]
        public void Should_ask_http_downloader_to_download_from_the_correct_url()
        {
            downloader.SetHtmlString("");
            service.Get10(1, r => {});

            Assert.AreEqual("http://services.smeedee.org/smeedee/MobileServices/LatestCommits/?fromRevision=1", downloader.UrlAskedFor);
        }
    }
}
