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

        [SetUp]
        public void SetUp()
        {
            SmeedeeApp.Instance.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            model = new LatestChangesets();
        }

        [Test]
        public void Should_call_callback_on_load()
        {
            var callbackCalled = false;
            model.Load(() => callbackCalled = true);
            Assert.IsTrue(callbackCalled);
        }
    }
}
