using System;

namespace Smeedee
{
    public interface IWorkingDaysLeftService
    {
        void Get(Action<int, DateTime> action, Action failureCallback);
    }
}
