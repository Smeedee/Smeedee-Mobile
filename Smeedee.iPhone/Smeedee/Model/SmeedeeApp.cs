using System;
using Smeedee.Services;

namespace Smeedee.Model
{
	public class SmeedeeApp
	{
		private SmeedeeApp()
		{
		}
		
		public static ISmeedeeService SmeedeeService = new SmeedeeHttpService();
		
		public static SmeedeeApp Instance = new SmeedeeApp();
	}
}
