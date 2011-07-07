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
                model = new WorkingDaysLeft(pair.Item1);

                Assert.AreEqual(pair.Item2, model.DaysLeftText);
            }
        }

    }

}
