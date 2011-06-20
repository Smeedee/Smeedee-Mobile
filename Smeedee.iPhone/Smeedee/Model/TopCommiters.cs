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
		
		public void Load(Action callback)
		{
			callback();
		}
	}
	
	public class TopCommiter
	{
	}
}
