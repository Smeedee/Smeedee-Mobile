using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Utilities;

namespace Smeedee.UnitTests
{
    [TestFixture]
    public class TimeSpanPrettyPrintExtensionTests
    {
        /* This class is taken from what StackOverflow uses, so we can presume that works.
         * What's new is the handling of negative time spans
         */
        [Test]
        public void Should_handle_negative_timespans_of_2_days()
        {
            var ts = new TimeSpan(2, 0, 0, 0).Negate();
            Assert.AreEqual("2 days into the future", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_negative_timespans_of_1_day()
        {
            var ts = new TimeSpan(1, 0, 0, 0).Negate();
            Assert.AreEqual("tomorrow", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_negative_timespans_of_1_hour()
        {
            var ts = new TimeSpan(0, 1, 0, 0).Negate();
            Assert.AreEqual("an hour into the future", ts.PrettyPrint());
        }


        [Test]
        public void Should_handle_negative_timespans_of_1_minute()
        {
            var ts = new TimeSpan(0, 0, 1, 0).Negate();
            Assert.AreEqual("a minute into the future", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_negative_timespans_of_1_second()
        {
            var ts = new TimeSpan(0, 0, 0, 1).Negate();
            Assert.AreEqual("one second into the future", ts.PrettyPrint());
        }

    }
}
