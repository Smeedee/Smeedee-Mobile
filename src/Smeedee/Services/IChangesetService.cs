using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface IChangesetService
    {
        IEnumerable<Changeset> GetLatestChangesets();
    }
}
