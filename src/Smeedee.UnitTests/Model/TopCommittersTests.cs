using System;
using System.ComponentModel;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee;

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
        public void Should_have_list_of_committers_after_load()
        {
            model.Load(() => { });

            Assert.AreEqual(2, model.Committers.Count());
        }

    }
}
