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
    public class WorkingDaysLeftTests
    {
        private SmeedeeApp app = SmeedeeApp.Instance;

        [SetUp]
        public void SetUp()
        {
            app.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            app.ServiceLocator.Bind<IPersistenceService>(new FakePersistenceService());
        }

        private void SetData(int daysLeft, DateTime deadline)
        {
            var data = new List<string[]> {new[] {daysLeft.ToString(), deadline.ToString()}};
            app.ServiceLocator.Bind<IFetchHttp>(new FakeHttpFetcher(Csv.ToCsv(data)));
        }

        [Test]
        public void Should_call_callback_on_load()
        {
            var callbackCalled = false;
            SetData(1, DateTime.Now);
            new WorkingDaysLeftService().Get((daysLeft, date) => callbackCalled = true);
            Assert.IsTrue(callbackCalled);
        }

        [Test]
        public void Should_call_callback_even_on_http_failure()
        {
            app.ServiceLocator.Bind<IFetchHttp>(new FakeHttpFetcher(""));
            var callbackCalled = false;
            new WorkingDaysLeftService().Get((daysLeft, date) => callbackCalled = true);
            Assert.IsTrue(callbackCalled);
        }
    }
}
