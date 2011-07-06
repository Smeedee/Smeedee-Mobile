using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Services
{
    public interface IWorkingDaysLeftService
    {
        void GetNumberOfWorkingDaysLeft(Action<int> callback);
    }
}
