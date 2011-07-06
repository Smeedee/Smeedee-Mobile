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
        }
    }
}
