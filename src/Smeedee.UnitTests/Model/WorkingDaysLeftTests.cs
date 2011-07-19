using System;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class WorkingDaysLeftTests
    {
        private WorkingDaysLeftFakeService fakeService;
        private WorkingDaysLeft model;

        [SetUp]
        public void SetUp()
        {
            SmeedeeApp.Instance.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            fakeService = new WorkingDaysLeftFakeService();
            SmeedeeApp.Instance.ServiceLocator.Bind<IWorkingDaysLeftService>(fakeService);
            model = new WorkingDaysLeft();
        }

        private void SetServiceData(int daysLeft, DateTime untilDate)
        {
            fakeService.DaysLeft = daysLeft;
            fakeService.UntilDate = untilDate;
        }

        [TestCase(0, "working days left")]
        [TestCase(1, "working day left")]
        [TestCase(42, "working days left")]
        public void Should_return_the_correct_suffix_for_positive_number_of_days(int days, string expected)
        {
            SetServiceData(days, new DateTime(2000, 1, 1));
            model.Load(() => { });
            Assert.AreEqual(expected, model.DaysLeftText);
        }

        [TestCase(-1, "day on overtime")]
        [TestCase(-2, "days on overtime")]
        public void Should_return_overtime_suffix_for_negative_number_of_days(int days, string expected)
        {
            SetServiceData(days, new DateTime(2000, 1, 1));
            model.Load(() => { });
            Assert.AreEqual(expected, model.DaysLeftText);
        }

        [Test]
        public void Should_return_absolute_value_of_days()
        {
            SetServiceData(-1, new DateTime(2000, 1, 1));
            model.Load(() => { });

            Assert.AreEqual(1, model.DaysLeft);
        }

        [TestCase(-1, true)]
        [TestCase(-42, true)]
        public void Should_set_IsOnOvertime_bool_to_true_if_negative_number_of_days(int days, bool expected)
        {
            SetServiceData(days, new DateTime(2000, 1, 1));
            model.Load(() => { });

            string notUsed = model.DaysLeftText;
            Assert.AreEqual(expected, model.IsOnOvertime);
        }

        [TestCase(1, false)]
        [TestCase(42, false)]
        public void Should_set_IsOnOvertime_bool_to_false_if_positive_number_of_days(int days, bool expected)
        {
            SetServiceData(days, new DateTime(2000, 1, 1));
            model.Load(() => { });

            string notUsed = model.DaysLeftText;
            Assert.AreEqual(expected, model.IsOnOvertime);
        }
    }
}
