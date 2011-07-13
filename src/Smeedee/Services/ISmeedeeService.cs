using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface ISmeedeeService
    {
        void LoadTopCommiters(Action<AsyncResult<IEnumerable<Committer>>> callback);
    }
}
