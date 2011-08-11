using System;
using System.Collections.Generic;
using NUnit.Framework;
using Smeedee.Lib;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Services
{
    [TestFixture]
    public class WorkingDaysLeftServiceTests
    {
        private SmeedeeApp app = SmeedeeApp.Instance;

        [SetUp]
        public void SetUp()
        {
            app.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            app.ServiceLocator.Bind<IPersistenceService>(new FakePersistenceService());
            app.ServiceLocator.Bind<ILog>(new FakeLogService());
        }

        private void SetData(int daysLeft, DateTime deadline)
        {
            var data = new List<string[]> {new[] {daysLeft.ToString(), deadline.ToString("yyyyMMddHHmmss")}};
            app.ServiceLocator.Bind<IFetchHttp>(new FakeHttpFetcher(Csv.ToCsv(data)));
        }

        [Test]
        public void Should_call_callback_on_load()
        {
            var callbackCalled = false;
            SetData(1, DateTime.Now);
            new WorkingDaysLeftService().Get((daysLeft, date) => callbackCalled = true, () => {});
            Assert.IsTrue(callbackCalled);
        }

        [Test]
        public void Should_not_call_callback_on_http_failure()
        {
            app.ServiceLocator.Bind<IFetchHttp>(new FakeHttpFetcher(""));
            var callbackCalled = false;
            new WorkingDaysLeftService().Get((daysLeft, date) => callbackCalled = true, () => { });
            Assert.IsFalse(callbackCalled);
        }

        [Test]
        public void Should_call_error_callback_on_http_failure()
        {
            app.ServiceLocator.Bind<IFetchHttp>(new FakeHttpFetcher(""));
            var errorCallbackCalled = false;
            new WorkingDaysLeftService().Get((daysLeft, date) => { }, () => errorCallbackCalled = true);
            Assert.IsTrue(errorCallbackCalled);
        }

        [Test]
        public void Should_call_error_callback_on_malformed_number_of_days_left()
        {
            app.ServiceLocator.Bind<IFetchHttp>(new FakeHttpFetcher("notAnInt\f"+DateTime.Now.ToString()));
            var errorCallbackCalled = false;
            new WorkingDaysLeftService().Get((daysLeft, date) => { }, () => errorCallbackCalled = true);
            Assert.IsTrue(errorCallbackCalled);
        }

        [Test]
        public void Should_call_error_callback_on_malformed_deadline()
        {
            app.ServiceLocator.Bind<IFetchHttp>(new FakeHttpFetcher("1\fnotADateTime"));
            var errorCallbackCalled = false;
            new WorkingDaysLeftService().Get((daysLeft, date) => { }, () => errorCallbackCalled = true);
            Assert.IsTrue(errorCallbackCalled);
        }
    }
}
