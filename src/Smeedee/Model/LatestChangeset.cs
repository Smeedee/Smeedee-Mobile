using System.Collections.Generic;

namespace Smeedee.Model
{
    public class LatestChangeset : IModel
    {
        public IEnumerable<Changeset> Changesets { get; private set; }

        public LatestChangeset(IEnumerable<Changeset> changesets)
        {
            Changesets = changesets;
        }
    }
}