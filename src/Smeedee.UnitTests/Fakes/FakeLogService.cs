using Smeedee.Services;

namespace Smeedee.UnitTests.Fakes
{
    public class FakeLogService : ILog
    {
        public void Log(string message, params string[] more)
        {
        }
    }
}
