using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee;

namespace Smeedee.UnitTests
{
    [TestFixture]
    public class GuardTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNull_should_throw_ArgumentNullException_on_null_arguments()
        {
            Guard.NotNull("notnull", new Object(), null);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotNull_should_throw_ArgumentNullException_on_null_arguments_no_matter_the_argument_order()
        {
            Guard.NotNull(null, "notnull", new Object());
        }

        [Test]
        public void NotNull_should_accept_non_null_arguments()
        {
            Guard.NotNull("notnull", "notnull either");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotNullOrEmpty_should_throw_ArgumentException_on_null_arguments()
        {
            Guard.NotNullOrEmpty("notnull", "notnull either", null, "dd", "bla");
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void NotNullOrEmpty_should_throw_ArgumentException_on_empty_arguments()
        {
            Guard.NotNullOrEmpty("notnull", "notnull either", "", "dd", "bla");
        }

        [Test]
        public void NotNullOrEmpty_should_accept_non_empty_strings()
        {
            Guard.NotNullOrEmpty("notnull", "notnull either");
        }
    }
}
