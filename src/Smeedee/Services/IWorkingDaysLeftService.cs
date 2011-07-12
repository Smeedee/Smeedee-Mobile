using System;

namespace Smeedee.Services
{
    public interface IWorkingDaysLeftService
    {
        void GetNumberOfWorkingDaysLeft(Action<int> callback);
    }
}
