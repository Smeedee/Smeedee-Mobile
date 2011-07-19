using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface ILatestChangesetsService
    {
        void Get(Action<IEnumerable<Changeset>> callback);
        void Get(int count, Action<IEnumerable<Changeset>> callback);
    }
}