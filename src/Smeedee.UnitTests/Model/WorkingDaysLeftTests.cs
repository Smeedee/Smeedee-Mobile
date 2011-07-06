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

        [Test]
        public void Should_get_the_correct_string_for_every_number_of_days()
        {
            var correctPairs = new[] {
                Tuple.Create(0, "working days left"),
                Tuple.Create(1, "working day left"),
                Tuple.Create(2, "working days left"),
                Tuple.Create(10, "working days left"),
            };

            foreach (var pair in correctPairs)
            {
                WorkingDaysLeftFakeService.FakeDays = pair.Item1;
                model.Load(() => { });

                Assert.AreEqual(pair.Item2, model.DaysLeftText);
            }
        }
    }

    public class WorkingDaysLeftFakeService : IWorkingDaysLeftService
    {
        private IBackgroundWorker worker;

        public static int FakeDays { get; set; }

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
