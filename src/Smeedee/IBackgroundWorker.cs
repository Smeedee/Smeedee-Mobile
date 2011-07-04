using System;

namespace Smeedee
{
    public interface IBackgroundWorker
    {
        void Invoke(Action fn);
    }
}