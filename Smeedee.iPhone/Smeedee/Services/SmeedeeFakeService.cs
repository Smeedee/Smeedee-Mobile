using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
	public class SmeedeeFakeService : ISmeedeeService
	{
		public SmeedeeFakeService()
		{
		}

		public IEnumerable<Commiter> LoadTopCommiters()
		{
			return new [] {
				new Commiter("Alex"),
				new Commiter("Dag Olav"),
				new Commiter("Lars 1"),
				new Commiter("Lars 2"),
				new Commiter("Børge"),
			};
		}
	}
}
