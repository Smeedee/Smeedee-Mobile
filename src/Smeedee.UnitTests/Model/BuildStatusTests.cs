using System;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Model
{
    public class BuildStatusTests
    {
        protected const string PROJECT = "test project";
        protected const string USER = "test user";
        protected readonly DateTime DATE = DateTime.MinValue;
        protected SmeedeeApp smeedeeApp = SmeedeeApp.Instance;

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

        [TestFixture]
        public class When_asking_for_ordered_builds : BuildStatusTests
        {
            [SetUp]
            public void SetUp()
            {
                smeedeeApp.ServiceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService(new NoBackgroundInvokation()));
            }
        }
    }
}
