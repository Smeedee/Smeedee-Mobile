using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface ILatestCommitsService
    {
        void Get(Action<IEnumerable<Commit>> callback);
        void Get(int count, Action<IEnumerable<Commit>> callback);
    }
}