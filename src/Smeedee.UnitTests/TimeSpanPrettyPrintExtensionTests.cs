using System;
using NUnit.Framework;

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
            Assert.AreEqual("recent", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_negative_timespans_of_1_day()
        {
            var ts = new TimeSpan(1, 0, 0, 0).Negate();
            Assert.AreEqual("recent", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_negative_timespans_of_1_hour()
        {
            var ts = new TimeSpan(0, 1, 0, 0).Negate();
            Assert.AreEqual("recent", ts.PrettyPrint());
        }


        [Test]
        public void Should_handle_negative_timespans_of_1_minute()
        {
            var ts = new TimeSpan(0, 0, 1, 0).Negate();
            Assert.AreEqual("recent", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_negative_timespans_of_1_second()
        {
            var ts = new TimeSpan(0, 0, 0, 1).Negate();
            Assert.AreEqual("recent", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_positive_timespans_in_the_singular_minute_category()
        {
            var ts = new TimeSpan(0, 0, 1, 0);
            Assert.AreEqual("a minute ago", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_positive_timespans_in_the_minutes_category()
        {
            var ts = new TimeSpan(0, 0, 32, 0);
            Assert.AreEqual("32 minutes ago", ts.PrettyPrint());
        }
        
        [Test]
        public void Should_handle_positive_timespans_in_the_singular_hour_category()
        {
            var ts = new TimeSpan(0, 1, 0, 0);
            Assert.AreEqual("an hour ago", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_positive_timespans_in_the_hours_category()
        {
            var ts = new TimeSpan(0, 12, 0, 0);
            Assert.AreEqual("12 hours ago", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_positive_timespans_in_the_singular_month_category()
        {
            var ts = new TimeSpan(31, 0, 0, 0);
            Assert.AreEqual("one month ago", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_positive_timespans_in_the_months_category()
        {
            var ts = new TimeSpan(64, 0, 0, 0);
            Assert.AreEqual("2 months ago", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_positive_timespans_in_the_singular_year_category()
        {
            var ts = new TimeSpan(365, 0, 0, 0);
            Assert.AreEqual("one year ago", ts.PrettyPrint());
        }

        [Test]
        public void Should_handle_positive_timespans_in_the_years_category()
        {
            var ts = new TimeSpan(800, 0, 0, 0);
            Assert.AreEqual("2 years ago", ts.PrettyPrint());
        }
    }
}
