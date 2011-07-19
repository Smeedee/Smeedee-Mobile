using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface ILatestChangesetsService
    {
        List<Changeset> Get();
        List<Changeset> Get(int count);
    }
}