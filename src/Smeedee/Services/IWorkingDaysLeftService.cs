using System;

namespace Smeedee
{
    public interface IWorkingDaysLeftService
    {
        void GetNumberOfWorkingDaysLeft(Action<int> callback);
    }
}
