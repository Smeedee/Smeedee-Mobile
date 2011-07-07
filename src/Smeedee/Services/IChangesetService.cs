using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public interface IChangesetService
    {
        IEnumerable<Changeset> GetLatestChangesets();
    }
}
