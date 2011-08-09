namespace Smeedee.Services
{
	public interface ILog
	{
		void Log(string message, params string[] more);
	}
}