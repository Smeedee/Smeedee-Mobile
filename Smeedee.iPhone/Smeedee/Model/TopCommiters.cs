using System;
using System.Collections.Generic;

namespace Smeedee.Model
{
	public class TopCommiters
	{
		public TopCommiters()
		{
			Commiters = new List<TopCommiter>();
		}
		
		public IEnumerable<TopCommiter> Commiters {
			get;
			private set;
		}
	}
	
	public class TopCommiter
	{
	}
}
