﻿using System;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class BuildStatusServiceTests
    {
        protected FakeHtmlFetcher downloader;
        protected IBuildStatusService service;
        protected IBackgroundWorker bgWorker;

        [SetUp]
        public void SetUp()
        {
            downloader = new FakeHtmlFetcher("hello");
            bgWorker = new NoBackgroundInvocation();
            service = new BuildStatusService(downloader, bgWorker);
        }

        [Test]
        public void Should_correctly_parse_build_name()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f22.07.2011 09:29:32\a");
            service.Load((r) => Assert.AreEqual("TestProject", r.Result.First().ProjectName));
        }

        [Test]
        public void Should_correctly_parse_user_name()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f22.07.2011 09:29:32\a");
            service.Load(r => Assert.AreEqual("TestUser", r.Result.First().Username));
        }

        [Test]
        public void Should_correctly_parse_working_build_state()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f22.07.2011 09:29:32\a");
            service.Load(r => Assert.AreEqual(BuildState.Working, r.Result.First().BuildSuccessState));
        }

        [Test]
        public void Should_correctly_parse_broken_build_state()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fBroken\f22.07.2011 09:29:32\a");
            service.Load(r => Assert.AreEqual(BuildState.Broken, r.Result.First().BuildSuccessState));
        }

        [Test]
        public void Should_correctly_parse_unknown_build_state()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fUnknown\f22.07.2011 09:29:32\a");
            service.Load(r => Assert.AreEqual(BuildState.Unknown, r.Result.First().BuildSuccessState));
        }

        [Test]
        public void Should_correctly_parse_date()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f22.07.2011 09:29:32\a");
            service.Load(r => Assert.AreEqual(DateTime.Parse("22.07.2011 09:29:32"), r.Result.First().BuildTime));
        }

        [Test]
        public void Should_identify_and_parse_builds_following_a_newline_delimiter()
        {
            downloader.SetHtmlString("TestProject\fTestUser\fWorking\f22.07.2011 09:29:32\aTestProject2\fTestUser2\fWorking\f22.07.2011 09:29:32");
            service.Load(r => Assert.AreEqual("TestProject2", r.Result.Skip(1).First().ProjectName));
        }
    }
}
