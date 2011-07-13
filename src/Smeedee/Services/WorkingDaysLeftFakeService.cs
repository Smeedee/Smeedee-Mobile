using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    class WorkingDaysLeftFakeService : IModelService<WorkingDaysLeft>
    {
        private WorkingDaysLeft model = new WorkingDaysLeft(4, new DateTime(2011, 7, 15));

        public IEnumerable<WorkingDaysLeft> Get(IDictionary<string, string> args)
        {
            return new [] {model};
        }

        public WorkingDaysLeft GetSingle(IDictionary<string, string> args)
        {
            return model;
        }
    }
}
