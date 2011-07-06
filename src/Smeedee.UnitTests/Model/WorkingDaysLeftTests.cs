using System;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.Utilities;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class WorkingDaysLeftTests
    {
        private WorkingDaysLeft model;

        [SetUp]
        public void SetUp()
        {
            SmeedeeApp.Instance.ServiceLocator.Bind<IWorkingDaysLeftService>(
                new WorkingDaysLeftFakeService(new NoBackgroundWorker())
            );
            model = new WorkingDaysLeft();
        }

        [Test]
        public void Should_invoke_callback_on_load()
        {
            var callbackWasExecuted = false;

            model.Load(() =>
            {
                callbackWasExecuted = true;
            });

            Assert.IsTrue(callbackWasExecuted);
        }

        [Test]
        public void Should_fetch_data_from_service()
        {
            model.Load(() => { });

            Assert.AreEqual(42, model.DaysLeft);
        }
    }

    public class WorkingDaysLeftFakeService : IWorkingDaysLeftService
    {
        private IBackgroundWorker worker;

        public int FakeDays { get; set; }

        public WorkingDaysLeftFakeService(IBackgroundWorker worker)
        {
            this.worker = worker;
            FakeDays = 42;
        }

        public void GetNumberOfWorkingDaysLeft(Action<int> callback)
        {
            worker.Invoke(() => callback(FakeDays));
        }
    }

}
