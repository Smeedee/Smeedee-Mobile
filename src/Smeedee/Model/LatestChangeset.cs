using System.Collections.Generic;

namespace Smeedee.Model
{
    public class LatestChangeset : IModel
    {
        public List<Changeset> Changesets { get; private set; }

        public LatestChangeset(IEnumerable<Changeset> changesets)
        {
            Changesets = new List<Changeset>(changesets);
        }

    }
}