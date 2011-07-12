using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee.Services
{
    public interface ITopCommittersService
    {
        void LoadTopCommiters(Action<IEnumerable<Committer>> callback);
    }
}
