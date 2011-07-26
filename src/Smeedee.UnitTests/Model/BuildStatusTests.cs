using System;
using System.Collections.Generic;
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
        private readonly FakePersistenceService persister = new FakePersistenceService();
        private readonly Fakes.FakeBuildStatusService buildStatusService = new Fakes.FakeBuildStatusService(new NoBackgroundInvocation());

        [SetUp]
        public void SetUp()
        {
            smeedeeApp.ServiceLocator.Bind<IBuildStatusService>(buildStatusService);
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
                model.Load(() => Assert.AreEqual(2, model.GetNumberOfBuildsByState(BuildState.Broken)));
            }

            [Test]
            public void Should_report_correct_amount_of_working_builds()
            {
                var model = new BuildStatus();
                model.Load(() => Assert.AreEqual(3, model.GetNumberOfBuildsByState(BuildState.Working)));
            }

            [Test]
            public void Should_report_correct_amount_of_unknown_builds()
            {
                var model = new BuildStatus();
                model.Load(() => Assert.AreEqual(3, model.GetNumberOfBuildsByState(BuildState.Unknown)));
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
            public void Should_properly_put_broken_builds_at_top_when_sorting_by_name()
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

            [Test]
            public void Should_properly_order_builds_by_build_time()
            {
                BuildStatus model = GetTimeSortedModel();
                model.Load(() => Assert.AreEqual(model.Builds.OrderBy(b => b.BuildTime), model.Builds));
            }

            [Test]
            public void Should_properly_put_broken_builds_at_top_when_sorting_by_time()
            {
                BuildStatus model = GetTimeSortedModelWithBrokenAtTop();
                model.Load(() => Assert.True(model.Builds.Take(2).Where(b => b.BuildSuccessState == BuildState.Broken).Count() == 2));
            }

            [Test]
            public void Should_maintain_time_based_ordering_among_broken_builds_when_they_are_on_top()
            {
                var model = GetTimeSortedModelWithBrokenAtTop();
                model.Load(() => Assert.AreEqual(model.Builds.Take(2).OrderBy(b => b.BuildTime), model.Builds.Take(2)));
            }

            [Test]
            public void Should_maintain_time_based_ordering_among_working_builds_when_broken_builds_are_on_top()
            {
                var model = GetTimeSortedModelWithBrokenAtTop();
                model.Load(() => Assert.AreEqual(model.Builds.Skip(2).Take(3).OrderBy(b => b.BuildTime), model.Builds.Skip(2).Take(3)));
            }

            [Test]
            public void Should_maintain_time_based_ordering_among_unknown_builds_when_broken_builds_are_on_top()
            {
                var model = GetTimeSortedModelWithBrokenAtTop();
                model.Load(() => Assert.AreEqual(model.Builds.Skip(5).Take(3).OrderBy(b => b.BuildTime), model.Builds.Skip(5).Take(3)));
            }


            private static BuildStatus GetNameSortedModel()
            {
                return new BuildStatus { BrokenBuildsAtTop = false, Ordering = BuildOrder.BuildName };
            }

            private static BuildStatus GetNameSortedModelWithBrokenAtTop()
            {
                return new BuildStatus { BrokenBuildsAtTop = true, Ordering = BuildOrder.BuildName };
            }

            private static BuildStatus GetTimeSortedModel()
            {
                return new BuildStatus { BrokenBuildsAtTop = false, Ordering = BuildOrder.BuildTime };
            }

            private static BuildStatus GetTimeSortedModelWithBrokenAtTop()
            {
                return new BuildStatus { BrokenBuildsAtTop = true, Ordering = BuildOrder.BuildTime };
            }
        }

        [TestFixture]
        public class When_saving_preferences : BuildStatusTests
        {
            // NOTE: These constants are set to the same values as the keys used by the platforms.
            // If you change the values in the model, you should also change the values in the
            // platform specific code.
            private const string PREFERENCE_IMPLEMENTATION_BUILD_SORT_ORDERING = "BuildStatus.Sorting";
            private const string PREFERENCE_IMPLEMENTATION_SHOW_TRIGGERED_BY = "BuildStatus.ShowTriggeredBy";
            private const string PREFERENCE_IMPLEMENTATION_BROKEN_BUILDS_AT_TOP = "BuildStatus.BrokenFirst";

            [Test]
            public void Should_persist_broken_first_preference_as_bool_with_correct_key()
            {
                var model = new BuildStatus();
                model.BrokenBuildsAtTop = true;

                Assert.AreEqual(persister.Get(PREFERENCE_IMPLEMENTATION_BROKEN_BUILDS_AT_TOP, false), true);
            }

            [Test]
            public void Should_retrieve_broken_first_from_correct_key()
            {
                var model = new BuildStatus();
                persister.Save(PREFERENCE_IMPLEMENTATION_BROKEN_BUILDS_AT_TOP, false);

                Assert.AreEqual(false, model.BrokenBuildsAtTop);
            }

            [Test]
            public void Should_persist_sort_order_as_string_with_correct_key()
            {
                var model = new BuildStatus();
                model.Ordering = BuildOrder.BuildName;

                Assert.AreNotEqual("foo", persister.Get(PREFERENCE_IMPLEMENTATION_BUILD_SORT_ORDERING, "foo"));
            }

            [Test]
            public void Should_retrieve_sort_order_from_correct_key()
            {
                var model = new BuildStatus();
                persister.Save(PREFERENCE_IMPLEMENTATION_BUILD_SORT_ORDERING, "buildname");

                Assert.AreEqual(BuildOrder.BuildName, model.Ordering);
            }

            [Test]
            public void Should_persist_sort_order_by_name_with_correct_value()
            {
                new BuildStatus {Ordering = BuildOrder.BuildName};

                Assert.AreEqual("buildname", persister.Get(PREFERENCE_IMPLEMENTATION_BUILD_SORT_ORDERING, "foo"));
            }

            [Test]
            public void Should_persist_sort_order_by_datetime_with_correct_valye()
            {
                new BuildStatus {Ordering = BuildOrder.BuildTime};

                Assert.AreEqual("buildtime", persister.Get(PREFERENCE_IMPLEMENTATION_BUILD_SORT_ORDERING, "foo"));
            }
			/*
            [Test]
            public void Should_persist_whether_name_should_be_shown_as_bool_with_correct_key()
            {
                new BuildStatus {ShowTriggeredBy = true};

                Assert.AreNotEqual(false, persister.Get(PREFERENCE_IMPLEMENTATION_SHOW_TRIGGERED_BY, false));
            }

            [Test]
            public void Should_retrieve_whether_name_should_be_shown_from_correct_key()
            {
                persister.Save(PREFERENCE_IMPLEMENTATION_SHOW_TRIGGERED_BY, true);
                var model = new BuildStatus();
                
                Assert.AreEqual(true, model.ShowTriggeredBy);
            }*/
        }

        [TestFixture]
        public class When_retrieving_dynamic_description : BuildStatusTests
        {
            [Test]
            public void Should_correctly_identify_and_present_number_of_builds_for_each_status()
            {
                var model = new BuildStatus();
                buildStatusService.Builds = buildStatusService.DefaultBuilds;
                model.Load(() => Assert.AreEqual("3 working, 2 broken, 3 unknown builds", model.DynamicDescription));
            }

            [Test]
            public void Should_correctly_present_empty_build_list()
            {
                var model = new BuildStatus();
                Assert.AreEqual("No builds fetched from the Smeedee Server", model.DynamicDescription);
            }

            [Test]
            public void Should_correctly_present_all_builds_working()
            {
                var model = new BuildStatus();
                buildStatusService.Builds = new List<Build>() {new Build("foo", BuildState.Working, "fooname", DateTime.Now), new Build("bar", BuildState.Working, "barname", DateTime.Now)};
                model.Load(() => Assert.AreEqual("2 working builds", model.DynamicDescription));
            }

            [Test]
            public void Should_correctly_present_all_builds_broken()
            {
                var model = new BuildStatus();
                buildStatusService.Builds = new List<Build>()
                                                {new Build("foo", BuildState.Broken, "fooname", DateTime.Now),
                                                new Build("bar", BuildState.Broken, "barname", DateTime.Now)};
                model.Load(() => Assert.AreEqual("OMG! All builds are broken!", model.DynamicDescription));
            }

            [Test]
            public void Should_correct_present_all_builds_unknown()
            {
                var model = new BuildStatus();
                buildStatusService.Builds = new List<Build>()
                                                {
                                                    new Build("foo", BuildState.Unknown, "fooname", DateTime.Now),
                                                    new Build("bar", BuildState.Unknown, "barname", DateTime.Now)
                                                };
                model.Load(() => Assert.AreEqual("2 unknown builds", model.DynamicDescription));
            }
        }
    }
}
