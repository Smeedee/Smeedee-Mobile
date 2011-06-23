using System;
using System.Linq;
using NUnit.Framework;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests.Model
{
	public class SmeedeeAppTests
	{
		[TestFixture]
		public class When_in_default_state
		{
			[Test]
			public void Then_assure_default_service_is_the_HTTP_service()
			{
				var httpServiceType = typeof(SmeedeeHttpService);
				var defaultServiceType = SmeedeeApp.SmeedeeService.GetType();
				
				Assert.AreEqual(httpServiceType.Name, defaultServiceType.Name);
			}
		}
	}
}
