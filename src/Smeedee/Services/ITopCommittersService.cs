using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Smeedee.Model;

namespace Smeedee.Services
{
    public interface ITopCommittersService
    {
        void LoadTopCommiters(Action<AsyncResult<IEnumerable<Committer>>> callback);
    }
}
