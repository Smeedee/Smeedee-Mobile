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
        private SmeedeeApp app = SmeedeeApp.Instance;

        [SetUp]
        public void SetUp()
        {
            downloader = new FakeHttpFetcher("foo");
            app.ServiceLocator.Bind<IFetchHttp>(downloader);
            app.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            app.ServiceLocator.Bind<IPersistenceService>(new FakePersistenceService());
            app.ServiceLocator.Bind<ILog>(new FakeLogService());
            service = new LatestCommitsService();
        }

        [Test]
        public void Should_correctly_commit_message()
        {
            downloader.SetHtmlString("TestMessage\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser\f1\fhttp://www.example.com/img.png");
            service.Get10Latest(r => Assert.AreEqual("TestMessage", r.First().Message));
        }

        [Test]
        public void Should_correctly_parse_user_name()
        {
            downloader.SetHtmlString("TestMessage\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser\f1\fhttp://www.example.com/img.png");
            service.Get10Latest(r => Assert.AreEqual("TestUser", r.First().User));
        }

        [Test]
        public void Should_correctly_parse_date()
        {
            var time = new DateTime(2011, 1, 1, 1, 1, 1);
            downloader.SetHtmlString("TestMessage\f" + time.ToString("yyyyMMddHHmmss") + "\fTestUser\f1\fhttp://www.example.com/img.png");
            service.Get10Latest(r => Assert.AreEqual(time, r.First().Date));
        }

        [Test]
        public void Should_only_return_one_line_if_there_is_no_line_delimiter()
        {
            downloader.SetHtmlString("TestMessage\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser\f1\fhttp://www.example.com/img.png");
            service.Get10Latest(r => Assert.AreEqual(1, r.Count()));
        }

        [Test]
        public void Should_identify_and_parse_commits_following_a_newline_delimiter()
        {
            downloader.SetHtmlString("TestMessage\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser\f1\fhttp://www.example.com/img.png\aTestMessage2\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser2\f2\fhttp://www.example.com/img.png");
            service.Get10Latest(r => Assert.AreEqual("TestMessage2", r.Skip(1).First().Message));
        }

        [Test]
        public void Should_gracefully_handle_malformed_http_results_by_returning_only_the_well_formed_lines_of_the_result()
        {
            downloader.SetHtmlString("TestMessage\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser\aBrokenTestMessage\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser\f1\fhttp://www.example.com/img.png\aTestMessage2\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser2\f2\fhttp://www.example.com/img.png");
            service.Get10Latest(r => Assert.AreEqual(2, r.Count()));
        }

        [Test]
        public void Should_ask_http_downloader_to_download_from_the_correct_url()
        {
            new Login {Url = "http://services.smeedee.org/smeedee", Key = "key123"};
            downloader.SetHtmlString("");
            service.Get10Latest(r => { });

            Assert.AreEqual("http://services.smeedee.org/smeedee/MobileServices/LatestCommits/?apiKey=key123", downloader.UrlAskedFor);
        }

        [Test]
        public void Should_ask_http_downloader_to_download_from_the_correct_url_when_looking_up_by_revision()
        {
            new Login {Url = "http://services.smeedee.org/smeedee", Key = "key123"};
            downloader.SetHtmlString("");
            service.Get10AfterRevision(10, r => { });

            Assert.AreEqual("http://services.smeedee.org/smeedee/MobileServices/LatestCommits/?apiKey=key123&revision=10", downloader.UrlAskedFor);
        }

        [Test]
        public void Should_strip_newlines_from_the_end_of_commit_messages()
        {
            downloader.SetHtmlString("TestMessage\n\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser\f1\fhttp://www.example.com/img.png\aTestMessage2\f" + DateTime.Now.ToString("yyyyMMddHHmmss") + "\fTestUser2\f2\fhttp://www.example.com/img.png");
            var msg = "";
            service.Get10Latest(r => msg = r.First().Message);
            Assert.AreEqual("TestMessage", msg);
        }
    }
}
