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
    }
}
