using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Model;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class CommitTests
    {
        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void First_constructor_argument_can_not_be_null()
        {
            new Commit(null, DateTime.Now, "larspars", 1);
        }

        [Test]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Third_constructor_argument_can_not_be_null()
        {
            new Commit("msg", DateTime.Now, null, 1);
        }

        [Test]
        public void Equals_methods_should_not_be_based_on_referential_equality_but_on_values()
        {
            var a = new Commit("msg", new DateTime(2011, 1, 1), "larspars", 1);
            var b = new Commit("msg", new DateTime(2011, 1, 1), "larspars", 1);
            Assert.IsTrue(a.Equals(b));
        }

        [Test]
        public void Equals_methods_should_return_false_when_values_are_not_equal()
        {
            var a = new Commit("msg", new DateTime(2011, 1, 1), "larspars", 1);
            var b = new Commit("msg2", new DateTime(2011, 1, 1), "larspars", 1);
            var c = new Commit("msg", new DateTime(2011, 1, 2), "larspars", 1);
            var d = new Commit("msg", new DateTime(2011, 1, 1), "larspars2", 1);
            Assert.IsFalse(a.Equals(b));
            Assert.IsFalse(a.Equals(c));
            Assert.IsFalse(a.Equals(d));
        }
    }
}
