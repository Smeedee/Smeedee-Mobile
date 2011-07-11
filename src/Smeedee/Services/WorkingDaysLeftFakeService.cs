using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    class WorkingDaysLeftFakeService : IModelService<WorkingDaysLeft>
    {
        private WorkingDaysLeft model = new WorkingDaysLeft(1);

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
