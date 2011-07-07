using System;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Model
{
    public class TopCommittersTests
    {
        [TestFixture]
        public class When_creating_a_new_TopCommitters_instance
        {
            [Test]
            public void Then_assure_there_are_no_committers()
            {
                var topCommiters = new TopCommitters();
                
                Assert.AreEqual(0, topCommiters.Committers.Count());
            }
        }
    }
}
