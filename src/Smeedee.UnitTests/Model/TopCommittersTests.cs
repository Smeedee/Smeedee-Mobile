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

        private IPersistenceService persistenceService;
        private ITopCommittersService topCommittersService;

        [SetUp]
        public void SetUp()
        {
            var serviceLocator = SmeedeeApp.Instance.ServiceLocator;

            persistenceService = new FakePersistenceService();
            topCommittersService = new TopCommittersFakeService();

            serviceLocator.Bind<IPersistenceService>(persistenceService);
            serviceLocator.Bind<ITopCommittersService>(topCommittersService);
            
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
            _model.NumberOfCommitters = 10;

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
            var interval = _model.TimePeriod;

            Assert.AreEqual(TimePeriod.PastDay, interval);
        }

        [Test]
        public void Should_get_different_results_for_different_time_periods()
        {
            var model1 = new TopCommitters();
            var model2 = new TopCommitters();
            model1.TimePeriod = TimePeriod.PastDay;
            model1.TimePeriod = TimePeriod.PastWeek;
            model1.Load(() => { });
            model2.Load(() => { });

            var list1 = model1.Committers;
            var list2 = model2.Committers;

            CollectionAssert.AreNotEqual(list1, list2);
        }

        [TestCase(5, TimePeriod.PastDay, "Top 5 committers for the past 24 hours")]
        [TestCase(10, TimePeriod.PastWeek, "Top 10 committers for the past week")]
        [TestCase(15, TimePeriod.PastMonth, "Top 15 committers for the past month")]
        public void Should_return_correct_description_after_load(int count, TimePeriod time, string expected)
        {
            _model.NumberOfCommitters = count;
            _model.TimePeriod = time;
            _model.Load(() => { });

            Assert.AreEqual(expected, _model.Description);
        }

        [Test]
        public void Should_save_number_of_committers()
        {
            _model.NumberOfCommitters = 42;

            Assert.AreEqual(1, (persistenceService as FakePersistenceService).SaveCalls);
        }

        [Test]
        public void Should_read_persistent_number_of_committers()
        {
            persistenceService.Save("TopCommitters.NumberOfCommitters", 42.ToString());

            Assert.AreEqual(42, _model.NumberOfCommitters);
        }

        [Test]
        public void Should_save_time_period()
        {
            _model.TimePeriod = TimePeriod.PastMonth;

            Assert.AreEqual(1, (persistenceService as FakePersistenceService).SaveCalls);
        }

        [Test]
        public void Should_read_persistent_time_period()
        {
            persistenceService.Save("TopCommitters.TimePeriod", TimePeriod.PastMonth.ToString());

            Assert.AreEqual(TimePeriod.PastMonth, _model.TimePeriod);
        }
    }
}
