using System;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;

namespace Smeedee.UnitTests.Model
{
	public class TopCommitersTests
	{
		[TestFixture]
		public class When_creating_a_new_TopCommiters_instance
		{
			[Test]
			public void Then_assure_there_are_no_commiters()
			{
				var topCommiters = new TopCommiters();
				
				Assert.AreEqual(0, topCommiters.Commiters.Count());
			}
		}
		
		[TestFixture]
		public class When_loading_TopCommiters
		{
			private TopCommiters topCommiters;
			
			[SetUp]
			public void SetUp()
			{
				SmeedeeApp.SmeedeeService = new FakeSmeedeeService();
				
				topCommiters = new TopCommiters();
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
				
				Assert.IsTrue(topCommiters.Commiters.Count() > 0);
			}
			
			[Test]
			public void Then_assure_that_commiters_have_real_data()
			{
				topCommiters.Load(() => { });
				
				foreach (var commiter in topCommiters.Commiters) {
					Assert.IsTrue(commiter.Name.Length > 0);
				}
			}
		}
	}
}
