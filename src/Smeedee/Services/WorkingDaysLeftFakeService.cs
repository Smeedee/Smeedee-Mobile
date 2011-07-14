using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    class WorkingDaysLeftFakeService : IModelService<WorkingDaysLeft>
    {
        public WorkingDaysLeft Get()
        {
            return new WorkingDaysLeft(-1, new DateTime(2011, 7, 15));
        }
		
        public WorkingDaysLeft Get(IDictionary<string, string> args)
        {
            return Get();
        }
    }
}
