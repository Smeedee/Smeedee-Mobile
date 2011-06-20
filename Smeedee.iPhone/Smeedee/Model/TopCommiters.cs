using System;
using System.Collections.Generic;
using Smeedee.Services;

namespace Smeedee.Model
{
	public class TopCommiters
	{
		private ISmeedeeService smeedeeService = SmeedeeApp.SmeedeeService;
		
		public TopCommiters()
		{
			Commiters = new List<TopCommiter>();
		}
		
		public List<TopCommiter> Commiters {
			get;
			private set;
		}
		
		public void Load(Action callback)
		{
			var data = smeedeeService.LoadTopCommiters();
			Commiters.AddRange(data);
			
			callback();
		}
	}
	
	public class TopCommiter
	{
	}
}
