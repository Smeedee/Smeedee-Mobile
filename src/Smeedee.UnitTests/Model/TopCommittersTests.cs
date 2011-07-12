using System;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class TopCommittersTests
    {
        private Committer[] singleCommitter = {
            new Committer("Lars", 17, "http://www.foo.com/img.png")
        };

        [Test]
        public void Should_implement_IModel()
        {
            Assert.That(typeof(IModel).IsAssignableFrom(typeof(TopCommitters)));
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_throw_exception_on_null_argument()
        {
            new TopCommitters(null, 0);
        }

        [TestCase(1, "Top committers for the past 24 hours")]
        [TestCase(2, "Top committers for the past 2 days")]
        [TestCase(7, "Top committers for the past week")]
        [TestCase(30, "Top committers for the past month")]
        public void Should_return_string_description_of_time_period(int days, string expected)
        {
            var model = new TopCommitters(singleCommitter, days);

            Assert.AreEqual(expected, model.DaysText);
        }

        [TestFixture]
        public class When_instanciating_top_committers_with_list_of_committers
        {
            private TopCommitters model;

            [SetUp]
            public void SetUp()
            {
                model = new TopCommitters(new[] {
                    new Committer("Lars", 17, "http://www.foo.com/img.png"),
                    new Committer("Dag Olav", 24, "http://www.foo.com/img.png"),
                    new Committer("Borge", 16, "http://www.foo.com/img.png")
                }, 7);
            }

            [Test]
            public void Should_make_accessible_list_of_committers_provided()
            {
                Assert.AreEqual(3, model.Committers.Count());
            }

            [Test]
            public void Should_return_list_in_sorted_order()
            {
                Assert.AreEqual(24, model.Committers.First().Commits);
                Assert.AreEqual(17, model.Committers.ElementAt(1).Commits);
            }
        }
    }
}
