using System;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Model
{
    public class CommitterTests
    {
        [TestFixture]
        public class When_initializing
        {
            [Test]
            [ExpectedException(typeof(ArgumentNullException))]
            public void Then_assure_the_name_is_validated()
            {
                new Committer(null);
            }

            [Test]
            public void Then_the_provided_name_should_be_accessible()
            {
                var committer = new Committer("John Doe");
                var name = committer.Name;

                Assert.AreEqual("John Doe", name);
            }
        }
    }
}
