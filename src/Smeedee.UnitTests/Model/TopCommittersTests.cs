using System;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Model
{
    public class TopCommittersTests
    {
        [TestFixture]
        public class When_creating_a_new_TopCommitters_instance
        {
            [Test]
            public void Then_assure_there_are_no_committers()
            {
                SmeedeeApp.Instance.ServiceLocator.Bind<ITopCommittersService>(new TopCommittersFakeService());
                var topCommiters = new TopCommitters();
                
                Assert.AreEqual(0, topCommiters.Committers.Count());
            }
        }
        
        [TestFixture]
        public class When_loading_TopCommitters
        {
            private TopCommitters topCommiters;
            
            [SetUp]
            public void SetUp()
            {
                SmeedeeApp.Instance.ServiceLocator.Bind<ITopCommittersService>(new TopCommittersFakeService());
                
                topCommiters = new TopCommitters();
            }
            
            [Test]
            public void Then_assure_the_callback_is_executed()
            {
                var callbackWasExecuted = false;
                
                topCommiters.Load(() => {
                    callbackWasExecuted = true;
                });
                
                Assert.IsTrue(callbackWasExecuted);
            }
            
            [Test]
            public void Then_assure_commiters_are_loaded_into_the_model()
            {
                topCommiters.Load(() => { });
                
                Assert.IsTrue(topCommiters.Committers.Count() > 0);
            }
            
            [Test]
            public void Then_assure_that_committers_have_real_data()
            {
                topCommiters.Load(() => { });
                
                foreach (var commiter in topCommiters.Committers) {
                    Assert.IsTrue(commiter.Name.Length > 0);
                }
            }
        }
    }
}
