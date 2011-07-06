﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smeedee.Services
{
    class WorkingDaysLeftFakeService : IWorkingDaysLeftService
    {
        public void GetNumberOfWorkingDaysLeft(Action<int> callback)
        {
            const int days = 1;
            callback(days);
        }
    }
}
