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
                    new Committer("Dag Olav", 19, "http://www.foo.com/img.png")
                });
            }

            [Test]
            public void Should_make_accessible_list_of_committers_provided()
            {
                Assert.AreEqual(2, model.Committers.Count());
            }
        }
    }
}
