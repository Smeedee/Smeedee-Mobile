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

        [Test]
        public void Should_return_the_correct_suffix_for_positive_number_of_days()
        {
            var correctPairs = new[] {
                Tuple.Create(0, "working days left"),
                Tuple.Create(1, "working day left"),
                Tuple.Create(2, "working days left"),
                Tuple.Create(10, "working days left"),
            };

            foreach (var pair in correctPairs)
            {
                model = new WorkingDaysLeft(pair.Item1);

                Assert.AreEqual(pair.Item2, model.DaysLeftText);
            }
        }

        [Test]
        public void Should_return_overtime_suffix_for_negative_number_of_days()
        {
            var correctPairs = new[] {
                Tuple.Create(-1, "day on overtime"),
                Tuple.Create(-2, "days on overtime")
            };

            foreach (var pair in correctPairs)
            {
                model = new WorkingDaysLeft(pair.Item1);

                Assert.AreEqual(pair.Item2, model.DaysLeftText);
            }
        }

        [Test]
        public void Should_return_absolute_value_of_days()
        {
            model = new WorkingDaysLeft(-1);
            Assert.AreEqual(1, model.DaysLeft);
        }

    }

}
