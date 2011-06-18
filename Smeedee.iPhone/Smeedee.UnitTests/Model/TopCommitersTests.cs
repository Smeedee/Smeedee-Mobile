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
	}
}
