using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
    public interface IChangesetService
    {
        IEnumerable<Changeset> GetLatestChangesets();
    }
}
