using System;
using NUnit.Framework;
using Smeedee.Model;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class BuildTests
    {
        protected const string PROJECT = "test project";
        protected const string USER = "test user";
        protected readonly DateTime DATE = DateTime.MinValue;
        
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