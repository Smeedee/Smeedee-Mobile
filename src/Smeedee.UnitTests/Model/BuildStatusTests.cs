using System;
using NUnit.Framework;
using Smeedee.Model;

namespace Smeedee.UnitTests.Model
{
    public class BuildStatusTests
    {
        protected const string PROJECT = "test project";
        protected const string USER = "test user";
        protected readonly DateTime DATE = DateTime.MinValue;

        [TestFixture]
        public class In_general : BuildStatusTests
        {
            [Test]
            public void Should_implement_IModel()
            {
                Assert.True(typeof (IModel).IsAssignableFrom(typeof (Build)));
            }
        }

        [TestFixture]
        public class When_instantiating : BuildStatusTests
        {
            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_not_allow_null_in_project_name()
            {
                var model = new Build(null, BuildState.Working, USER, DATE);
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_not_allow_empty_string_in_project_name()
            {
                var model = new Build("", BuildState.Working, USER, DATE);
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_not_allow_null_in_username()
            {
                var model = new Build(PROJECT, BuildState.Working, null, DATE);
            }

            [Test]
            [ExpectedException(typeof(ArgumentException))]
            public void Should_not_allow_empty_string_in_username()
            {
                var model = new Build(PROJECT, BuildState.Working, "", DATE);
            }

        }
    }
}
