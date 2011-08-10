using System;
using Smeedee.Services;

namespace Smeedee.iOS
{
	public class IphoneLogger : ILog
	{
		public void Log(string message, params string[] more)
		{
			Console.WriteLine(message + " " + string.Join(" ", more));
		}
	}
}