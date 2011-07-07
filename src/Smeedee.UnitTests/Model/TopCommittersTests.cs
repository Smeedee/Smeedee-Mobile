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

        [Test]
        public void Should_make_accessible_list_of_committers_provided()
        {
            var topCommitters = new TopCommitters(new[] {
                new Committer("Lars", 17, "http://www.foo.com/img.png"),
                new Committer("Dag Olav", 19, "http://www.foo.com/img.png")
            });

            Assert.AreEqual(2, topCommitters.Committers.Count());
        }
    }
}
