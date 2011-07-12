using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Model;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class ChangesetTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void First_constructor_argument_can_not_be_null()
        {
            new Changeset(null, DateTime.Now, "larspars");
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Third_constructor_argument_can_not_be_null()
        {
            new Changeset("msg", DateTime.Now, null);
        }
    }
}
