using System;

namespace Smeedee.Utilities
{
    public interface IBackgroundWorker
    {
        void Invoke(Action fn);
    }
}