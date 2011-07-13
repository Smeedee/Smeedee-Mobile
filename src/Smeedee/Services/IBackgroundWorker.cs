using System;

namespace Smeedee.Services
{
    public interface IBackgroundWorker
    {
        void Invoke(Action fn);
    }
}