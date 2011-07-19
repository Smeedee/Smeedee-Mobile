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
    public class LatestChangesetsTests
    {
        private LatestChangesets model;
        private CallCountingLatestChangesetsService countingService;

        [SetUp]
        public void SetUp()
        {
            countingService = new CallCountingLatestChangesetsService();
            SmeedeeApp.Instance.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            SmeedeeApp.Instance.ServiceLocator.Bind<ILatestChangesetsService>(new FakeLatestChangesetsService());
            model = new LatestChangesets();
        }

        [Test]
        public void Should_call_callback_on_load()
        {
            var callbackCalled = false;
            model.Load(() => callbackCalled = true);
            Assert.IsTrue(callbackCalled);
        }

        [Test]
        public void Should_call_service_on_load()
        {
            SmeedeeApp.Instance.ServiceLocator.Bind<ILatestChangesetsService>(countingService);
            new LatestChangesets().Load(() => { });
            Assert.AreEqual(1, countingService.GetCalls);
        }

        
        private class CallCountingLatestChangesetsService : ILatestChangesetsService
        {
            public int GetCalls;
            public void Get(Action<IEnumerable<Changeset>> callback)
            {
                GetCalls++;
            }

            public void Get(int count, Action<IEnumerable<Changeset>> callback)
            {
                GetCalls++;
            }
        }
    }
}
