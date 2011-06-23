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

		public IEnumerable<Commiter> LoadTopCommiters()
		{
			return new [] {
				new Commiter("John Doe"),
				new Commiter("Mary Poppins")
			};
		}
	}
}
