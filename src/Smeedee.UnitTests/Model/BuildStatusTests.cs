using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Model
{
    public class BuildStatusTests
    {
        protected SmeedeeApp smeedeeApp = SmeedeeApp.Instance;
        private FakePersistenceService persister = new FakePersistenceService();

        [SetUp]
        public void SetUp()
        {
            smeedeeApp.ServiceLocator.Bind<IBuildStatusService>(new FakeBuildStatusService(new NoBackgroundInvokation()));
            smeedeeApp.ServiceLocator.Bind<IPersistenceService>(persister);
        }

        [TestFixture]
        public class When_loading : BuildStatusTests
        {
            [Test]
            public void Should_invoke_callback()
            {
                var model = new BuildStatus();
                var hasBeenInvoked = false;
               
                model.Load(() => hasBeenInvoked = true);

                Assert.True(hasBeenInvoked);
            }
        }

        [TestFixture]
        public class When_asking_for_metainformation_about_builds : BuildStatusTests
        {
            [Test]
            public void Should_report_correct_amount_of_broken_builds()
            {
                var model = new BuildStatus();
                model.Load(() => Assert.AreEqual(2, model.GetNumberOfBuildsThatHaveState(BuildState.Broken)));
            }

            [Test]
            public void Should_report_correct_amount_of_working_builds()
            {
                var model = new BuildStatus();
                model.Load(() => Assert.AreEqual(3, model.GetNumberOfBuildsThatHaveState(BuildState.Working)));
            }

            [Test]
            public void Should_report_correct_amount_of_unknown_builds()
            {
                var model = new BuildStatus();
                model.Load(() => Assert.AreEqual(3, model.GetNumberOfBuildsThatHaveState(BuildState.Unknown)));
            }
        }

        [TestFixture]
        public class When_asking_for_builds : BuildStatusTests
        {
            [Test]
            public void Should_properly_order_builds_alphabetically()
            {
                var model = GetNameSortedModel();
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
                var model = GetNameSortedModelWithBrokenAtTop();
                model.Load(() => Assert.True(model.Builds.Take(2).Where(b => b.BuildSuccessState == BuildState.Broken).Count() == 2));
            }

            [Test]
            public void Should_maintain_alphabetical_ordering_among_broken_builds_when_they_are_on_top()
            {
                var model = GetNameSortedModelWithBrokenAtTop();
                model.Load(() => Assert.AreEqual(model.Builds.Take(2).OrderBy(b => b.ProjectName), model.Builds.Take(2)));
            }

            [Test]
            public void Should_maintain_alphabetical_ordering_among_working_builds_when_broken_builds_are_on_top()
            {
                var model = GetNameSortedModelWithBrokenAtTop();
                model.Load(() => Assert.AreEqual(model.Builds.Skip(2).Take(3).OrderBy(b => b.ProjectName), model.Builds.Skip(2).Take(3)));
            }

            [Test]
            public void Should_maintain_alphabetical_ordering_among_unknown_builds_when_broken_builds_are_on_top()
            {
                var model = GetNameSortedModelWithBrokenAtTop();
                model.Load(() => Assert.AreEqual(model.Builds.Skip(5).Take(3).OrderBy(b => b.ProjectName), model.Builds.Skip(5).Take(3)));
            }

            private static BuildStatus GetNameSortedModel()
            {
                return new BuildStatus { BrokenBuildsAtTop = false, Ordering = BuildOrder.BuildName };
            }

            private static BuildStatus GetNameSortedModelWithBrokenAtTop()
            {
                return new BuildStatus { BrokenBuildsAtTop = true, Ordering = BuildOrder.BuildName };
            }
        }

        [TestFixture]
        public class When_saving_preferences : BuildStatusTests
        {
            [Test]
            public void Should_persist_broken_first_preference_as_bool_with_correct_key()
            {
                var model = new BuildStatus();
                model.BrokenBuildsAtTop = true;

                Assert.AreEqual(persister.Get("brokenBuildsAtTop", false), true);
            }

            [Test]
            public void Should_persist_sort_order_as_string_with_correct_key()
            {
                var model = new BuildStatus();
                model.Ordering = BuildOrder.BuildName;

                Assert.AreNotEqual("foo", persister.Get("buildSortOrdering", "foo"));
            }

            [Test]
            public void Should_persist_sort_order_by_name_with_correct_value()
            {
                var model = new BuildStatus();
                model.Ordering = BuildOrder.BuildName;

                Assert.AreEqual("buildname", persister.Get("buildSortOrdering", "foo"));
            }

            [Test]
            public void Should_persist_sort_order_by_datetime_with_correct_valye()
            {
                var model = new BuildStatus();
                model.Ordering = BuildOrder.BuildTime;

                Assert.AreEqual("buildtime", persister.Get("buildSortOrdering", "foo"));
            }
        }
    }
}
