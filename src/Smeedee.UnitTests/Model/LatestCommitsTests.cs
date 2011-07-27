using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class LatestCommitsTests
    {
        private LatestCommits model;
        private CallCountingLatestCommitsService countingService;
        private UnitTests.Fakes.FakeLatestCommitsService fakeService;
        private SmeedeeApp app = SmeedeeApp.Instance;

        [SetUp]
        public void SetUp()
        {
            app.ServiceLocator.Bind<IBackgroundWorker>(new NoBackgroundInvocation());
            countingService = new CallCountingLatestCommitsService();
            fakeService = new UnitTests.Fakes.FakeLatestCommitsService();
            app.ServiceLocator.Bind<ILatestCommitsService>(fakeService);
            model = new LatestCommits();
        }

        [Test]
        public void Should_call_callback_on_load()
        {
            var callbackCalled = false;
            model.Load(() => callbackCalled = true);
            Assert.IsTrue(callbackCalled);
        }

        [Test]
        public void Should_call_service_on_load()
        {
            app.ServiceLocator.Bind<ILatestCommitsService>(countingService);
            new LatestCommits().Load(() => { });
            Assert.AreEqual(1, countingService.GetCalls);
        }

        [Test]
        public void Should_have_an_emtpy_list_on_construction()
        {
            Assert.NotNull(model.Commits);
        }

        [Test]
        public void Should_load_10_commits_on_first_load()
        {
            model.Load(() => { });
            Assert.AreEqual(10, model.Commits.Count());
        }

        [Test]
        public void Should_load_20_commits_after_Load_and_LoadMore()
        {
            model.Load(() => { });
            model.LoadMore(() => { });
            Assert.AreEqual(20, model.Commits.Count());
        }

        [Test]
        public void Should_not_duplicate_commits_when_new_commits_are_added_between_Load_and_LoadMore()
        {
            model.Load(() => { });

            var newData = new List<Commit> {new Commit("Commit msg", DateTime.Now, "larspars", 1)};
            newData.AddRange(fakeService.data);
            fakeService.data = newData;

            model.LoadMore(() => { });

            var hasDuplicates = false;
            foreach (var commit in model.Commits)
                if (model.Commits.FindAll(c => c.Equals(commit)).Count() > 1)
                    hasDuplicates = true;

            Assert.IsFalse(hasDuplicates);
            Assert.AreEqual(19, model.Commits.Count());
        }

        [Test]
        public void Should_call_callback_if_LoadMore_is_called_before_Load()
        {
            var wasCalled = false;
            model.LoadMore(() => wasCalled = true);
            Assert.True(wasCalled);
        }

        [Test]
        public void Should_have_an_empty_list_if_LoadMore_is_called_before_Load()
        {
            model.LoadMore(() => { });
            Assert.IsEmpty(model.Commits);
        }

        [Test]
        public void HasMore_property_should_be_true_initially()
        {
            Assert.True(model.HasMore);
        }

        [Test]
        public void HasMore_property_should_be_true_when_the_model_has_more()
        {
            model.Load(() => { });
            model.LoadMore(() => { });
            Assert.True(model.HasMore);
        }

        [Test]
        public void Should_set_HasMore_property_to_false_if_the_service_runs_out_of_commits()
        {
            model.Load(() => { });
            model.LoadMore(() => { });
            model.LoadMore(() => { });
            model.LoadMore(() => { });
            Assert.False(model.HasMore);
        }

        private class CallCountingLatestCommitsService : ILatestCommitsService
        {
            public int GetCalls;

            public void Get10AfterRevision(int fromIndex, Action<IEnumerable<Commit>> callback)
            {
                GetCalls++;
            }

            public void Get10Latest(Action<IEnumerable<Commit>> callback)
            {
                GetCalls++;
            }
        }
    }
}
