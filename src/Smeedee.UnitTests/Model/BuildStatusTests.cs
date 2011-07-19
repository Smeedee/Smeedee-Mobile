using System;
using System.Linq;
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
        public class When_asking_for_builds : BuildStatusTests
        {
            [SetUp]
            public void SetUp()
            {
                smeedeeApp.ServiceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService(new NoBackgroundInvokation()));
                smeedeeApp.ServiceLocator.Bind<IPersistenceService>(new FakePersistenceService());
            }

            [Test]
            public void Should_properly_order_builds_alphabetically()
            {
                var model = new BuildStatus {BrokenBuildsAtTop = false, Ordering = BuildOrder.BuildName};
                model.Load(() => Assert.AreEqual(model.Builds.OrderBy(b => b.ProjectName), model.Builds));
            }

            [Test]
            [Ignore] // TODO: Why is this stopping?
            public void Should_properly_order_builds_by_build_time()
            {
                var model = new BuildStatus {BrokenBuildsAtTop = false, Ordering = BuildOrder.BuildTime};
                model.Load(() => Assert.AreEqual(model.Builds.OrderBy(b => b.BuildTime), model.Builds));
            }

            [Test]
            public void Should_properly_put_broken_builds_at_top()
            {
                var model = new BuildStatus() {BrokenBuildsAtTop = true, Ordering = BuildOrder.BuildName};
                model.Load(() => Assert.True(model.Builds.Take(2).Where(b => b.BuildSuccessState == BuildState.Broken).Count() == 2));
            }
        }
    }
}
