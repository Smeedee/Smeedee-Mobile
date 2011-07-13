using System;
using NUnit.Framework;
using Smeedee.Model;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class WorkingDaysLeftTests
    {
        [TestCase(0, "working days left")]
        [TestCase(1, "working day left")]
        [TestCase(42, "working days left")]
        public void Should_return_the_correct_suffix_for_positive_number_of_days(int days, string expected)
        {
            var model = new WorkingDaysLeft(days, new DateTime(2000, 1, 1));

            Assert.AreEqual(expected, model.DaysLeftText);
        }

        [TestCase(-1, "day on overtime")]
        [TestCase(-2, "days on overtime")]
        public void Should_return_overtime_suffix_for_negative_number_of_days(int days, string expected)
        {
            var model = new WorkingDaysLeft(days, new DateTime(2000, 1, 1));

            Assert.AreEqual(expected, model.DaysLeftText);
        }

        [Test]
        public void Should_return_absolute_value_of_days()
        {
            var model = new WorkingDaysLeft(-1, new DateTime(2000, 1, 1));

            Assert.AreEqual(1, model.DaysLeft);
        }
        [TestCase(-1, true)]
        [TestCase(-42, true)]
        public void Should_set_IsOnOvertime_bool_to_true_if_negative_number_of_days(int days, bool expected)
        {
            var model = new WorkingDaysLeft(days, new DateTime(2000, 1, 1));
            string notUsed = model.DaysLeftText;
            Assert.AreEqual(expected, model.IsOnOvertime);
        }
        [TestCase(1, false)]
        [TestCase(42, false)]
        public void Should_set_IsOnOvertime_bool_to_false_if_positive_number_of_days(int days, bool expected)
        {
            var model = new WorkingDaysLeft(days, new DateTime(2000, 1, 1));
            string notUsed = model.DaysLeftText;
            Assert.AreEqual(expected, model.IsOnOvertime);
        }
    }
}
