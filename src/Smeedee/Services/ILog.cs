using System;

namespace Smeedee
{
	public interface ILog
	{
		void Log(string message, params string[] more);
	}
}

