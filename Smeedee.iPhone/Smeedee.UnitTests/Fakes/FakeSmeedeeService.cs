using System;
using System.Collections.Generic;
using Smeedee.Model;
using Smeedee.Services;

namespace Smeedee.UnitTests
{
	public class FakeSmeedeeService : ISmeedeeService
	{
		public FakeSmeedeeService()
		{
		}

		public IEnumerable<TopCommiter> LoadTopCommiters()
		{
			return new [] {
				new TopCommiter(),
				new TopCommiter()
			};
		}
	}
}
