using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface ILatestCommitsService
    {
        void Get10(int fromIndex, Action<IEnumerable<Commit>> callback);
    }
}