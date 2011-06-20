using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
	public interface ISmeedeeService
	{
		IEnumerable<Commiter> LoadTopCommiters();
	}
}
