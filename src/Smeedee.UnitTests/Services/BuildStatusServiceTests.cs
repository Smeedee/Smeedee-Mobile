using System;
using System.Globalization;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Services.Fakes;
using Smeedee.UnitTests.Fakes;
using FakeLogService = Smeedee.UnitTests.Fakes.FakeLogService;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class BuildStatusServiceTests
    {
        protected FakeHttpFetcher downloader;
        protected IBuildStatusService service;

        [SetUp]
        public void SetUp()
        {
            downloader = new FakeHttpFetcher("foo");
            SmeedeeApp.Instance.ServiceLocator.Bind<IFetchHttp>(downloader);
            SmeedeeApp.Instance.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            SmeedeeApp.Instance.ServiceLocator.Bind<IPersistenceService>(new FakePersistenceService());
            SmeedeeApp.Instance.ServiceLocator.Bind<IValidationService>(new FakeValidationService());
            SmeedeeApp.Instance.ServiceLocator.Bind<ILog>(new FakeLogService());
            service = new BuildStatusService();
        }

        [Test]
        public void Should_correctly_parse_build_name()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f20110722112921");
            service.Load(r => Assert.AreEqual("TestProject", r.First().ProjectName));
        }

        [Test]
        public void Should_correctly_parse_user_name()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f20110722112921");
            service.Load(r => Assert.AreEqual("TestUser", r.First().Username));
        }

        [Test]
        public void Should_correctly_parse_working_build_state()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f20110722112921");
            service.Load(r => Assert.AreEqual(BuildState.Working, r.First().BuildSuccessState));
        }

        [Test]
        public void Should_correctly_parse_broken_build_state()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fBroken\f20110722112921");
            service.Load(r => Assert.AreEqual(BuildState.Broken, r.First().BuildSuccessState));
        }

        [Test]
        public void Should_correctly_parse_unknown_build_state()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fUnknown\f20110722112921");
            service.Load(r => Assert.AreEqual(BuildState.Unknown, r.First().BuildSuccessState));
        }

        [Test]
        public void Should_correctly_parse_date()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f20110722112921");
            service.Load(r => Assert.AreEqual(DateTime.ParseExact("20110722112921", "yyyyMMddHHmmss", CultureInfo.InvariantCulture), r.First().BuildTime));
        }

        [Test]
        public void Should_only_return_one_line_if_there_is_no_line_delimiter()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f20110722112921");
            service.Load(r => Assert.AreEqual(1, r.Count()));
        }

        [Test]
        public void Should_identify_and_parse_builds_following_a_newline_delimiter()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f20110722112921\aTestProject2\fTestUser2\fWorking\f20110722112921");
            service.Load(r => Assert.AreEqual("TestProject2", r.Skip(1).First().ProjectName));
        }

        [Test]
        public void Should_gracefully_handle_malformed_http_results_by_returning_only_the_well_formed_lines_of_the_result()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f20110722112921\afoo\fmalformed\fbar\aTestProject2\fTestUser2\fWorking\f20110722112921");
            service.Load(r => Assert.AreEqual(2, r.Count()));
        }

        [Test]
        public void Should_ask_http_downloader_to_download_from_the_correct_url()
        {
            var login = new Login();
            login.Url = "http://services.smeedee.org/smeedee/";
            login.Key = "key123";
            downloader.SetHtmlString("");
            service.Load(r => { });

            Assert.AreEqual("http://services.smeedee.org/smeedee/MobileServices/BuildStatus/?apiKey=key123", 
                downloader.UrlAskedFor);
        }
    }
}
