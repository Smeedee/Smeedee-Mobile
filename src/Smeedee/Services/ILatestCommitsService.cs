using System;
using System.Collections.Generic;
using Smeedee.Model;

namespace Smeedee
{
    public interface ILatestCommitsService
    {
        void Get10AfterRevision(int fromRevision, Action<IEnumerable<Commit>> callback);
        void Get10Latest(Action<IEnumerable<Commit>> callback);
    }
}