using System;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;
using Smeedee.UnitTests.Fakes;

namespace Smeedee.UnitTests.Model
{
    [TestFixture]
    public class TopCommittersTests
    {
        private TopCommitters model;

        [SetUp]
        public void SetUp()
        {
            var serviceLocator = SmeedeeApp.Instance.ServiceLocator;
            serviceLocator.Bind<ITopCommittersService>(new TopCommittersFakeService());
            
            model = new TopCommitters();
        }

        [Test]
        public void Should_initially_have_empty_list_of_committers()
        {
            Assert.AreEqual(0, model.Committers.Count());
        }

        [Test]
        public void Should_run_callback_on_load()
        {
            var check = false;
            model.Load(() => check = true);

            Assert.IsTrue(check);
        }
        
        [Test]
        public void Should_have_list_of_default_five_committers_after_load()
        {
            model.Load(() => { });

            Assert.AreEqual(5, model.Committers.Count());
        }
        
        [Test]
        public void Should_be_able_to_set_number_of_committers()
        {
            model.Load(() => { });
            model.SetNumberOfCommitters(10);

            Assert.AreEqual(10, model.Committers.Count());
        }
        
        [Test]
        public void Should_return_list_of_committers_in_sorted_order()
        {
            model.Load(() => { });

            var committers = model.Committers;
            var sorted = model.Committers.OrderByDescending(c => c.Commits);

            CollectionAssert.AreEqual(sorted.ToList(), committers.ToList());
        }
        
        [Test]
        public void Should_have_default_time_interval_of_one_day()
        {
            var interval = model.GetTimeInterval();

            Assert.AreEqual(TopCommitters.TimeInterval.PastDay, interval);
        }
        
        [Test]
        public void Should_get_top_committer_with_42_commits_for_default_time_interval()
        {
            model.Load(() => { });

            var committer = model.Committers.First();
            Assert.AreEqual(42, committer.Commits);
        }

        [Test]
        public void Should_get_top_committer_with_62_commits_for_time_interval_of_one_week()
        {
            model.SetTimeInterval(TopCommitters.TimeInterval.PastWeek);
            model.Load(() => { });

            var committer = model.Committers.First();

            Assert.AreEqual(62, committer.Commits);
        }
        
    }
}
