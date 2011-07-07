using System;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class TopCommittersTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Should_throw_exception_on_null_argument()
        {
            var topCommiters = new TopCommitters(null);
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
                });
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
