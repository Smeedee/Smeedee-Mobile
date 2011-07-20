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
    public class LatestCommitsTests
    {
        private LatestCommits model;
        private CallCountingLatestCommitsService countingService;

        [SetUp]
        public void SetUp()
        {
            countingService = new CallCountingLatestCommitsService();
            SmeedeeApp.Instance.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            SmeedeeApp.Instance.ServiceLocator.Bind<ILatestCommitsService>(new FakeLatestCommitsService());
            model = new LatestCommits();
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
            SmeedeeApp.Instance.ServiceLocator.Bind<ILatestCommitsService>(countingService);
            new LatestCommits().Load(() => { });
            Assert.AreEqual(1, countingService.GetCalls);
        }

        [Test]
        public void Should_have_an_emtpy_list_on_construction()
        {
            Assert.NotNull(model.Commits);
        }
        
        private class CallCountingLatestCommitsService : ILatestCommitsService
        {
            public int GetCalls;
            public void Get(Action<IEnumerable<Commit>> callback)
            {
                GetCalls++;
            }

            public void Get(int count, Action<IEnumerable<Commit>> callback)
            {
                GetCalls++;
            }
        }
    }
}
