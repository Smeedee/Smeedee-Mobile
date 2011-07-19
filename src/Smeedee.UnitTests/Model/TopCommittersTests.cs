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
        private TopCommitters _model;

        [SetUp]
        public void SetUp()
        {
            var serviceLocator = SmeedeeApp.Instance.ServiceLocator;
            serviceLocator.Bind<ITopCommittersService>(new TopCommittersFakeService());
            
            _model = new TopCommitters();
        }

        [Test]
        public void Should_initially_have_empty_list_of_committers()
        {
            Assert.AreEqual(0, _model.Committers.Count());
        }

        [Test]
        public void Should_run_callback_on_load()
        {
            var check = false;
            _model.Load(() => check = true);

            Assert.IsTrue(check);
        }
        
        [Test]
        public void Should_have_list_of_default_five_committers_after_load()
        {
            _model.Load(() => { });

            Assert.AreEqual(5, _model.Committers.Count());
        }
        
        [Test]
        public void Should_be_able_to_set_number_of_committers()
        {
            _model.Load(() => { });
            _model.SetNumberOfCommitters(10);

            Assert.AreEqual(10, _model.Committers.Count());
        }
        
        [Test]
        public void Should_return_list_of_committers_in_sorted_order()
        {
            _model.Load(() => { });

            var committers = _model.Committers;
            var sorted = _model.Committers.OrderByDescending(c => c.Commits);

            CollectionAssert.AreEqual(sorted.ToList(), committers.ToList());
        }
        
        [Test]
        public void Should_have_default_time_interval_of_one_day()
        {
            var interval = _model.GetTimeInterval();

            Assert.AreEqual(TopCommitters.TimeInterval.PastDay, interval);
        }

        [Test]
        public void Should_get_different_results_for_different_time_periods()
        {
            var model1 = new TopCommitters();
            var model2 = new TopCommitters();
            model1.SetTimeInterval(TopCommitters.TimeInterval.PastDay);
            model1.SetTimeInterval(TopCommitters.TimeInterval.PastWeek);
            model1.Load(() => { });
            model2.Load(() => { });

            var list1 = model1.Committers;
            var list2 = model2.Committers;

            CollectionAssert.AreNotEqual(list1, list2);
        }

        [TestCase(TopCommitters.TimeInterval.PastDay, "Top committers for the past 24 hours")]
        [TestCase(TopCommitters.TimeInterval.PastWeek, "Top committers for the past week")]
        [TestCase(TopCommitters.TimeInterval.PastMonth, "Top committers for the past month")]
        public void Should_return_correct_description_for_time_period_after_load(TopCommitters.TimeInterval interval, string expected)
        {
            _model.SetTimeInterval(interval);
            _model.Load(() => { });

            Assert.AreEqual(expected, _model.Description);
        }

    }
}
